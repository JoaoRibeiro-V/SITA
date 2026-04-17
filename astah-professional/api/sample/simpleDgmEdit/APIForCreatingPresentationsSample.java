import java.awt.geom.Point2D;
import java.io.IOException;

import com.change_vision.jude.api.inf.editor.BasicModelEditor;
import com.change_vision.jude.api.inf.editor.ClassDiagramEditor;
import com.change_vision.jude.api.inf.editor.ModelEditorFactory;
import com.change_vision.jude.api.inf.editor.TransactionManager;
import com.change_vision.jude.api.inf.exception.InvalidEditingException;
import com.change_vision.jude.api.inf.exception.LicenseNotFoundException;
import com.change_vision.jude.api.inf.exception.ProjectLockedException;
import com.change_vision.jude.api.inf.exception.ProjectNotFoundException;
import com.change_vision.jude.api.inf.model.IAssociation;
import com.change_vision.jude.api.inf.model.IClass;
import com.change_vision.jude.api.inf.model.IGeneralization;
import com.change_vision.jude.api.inf.model.IModel;
import com.change_vision.jude.api.inf.model.IPackage;
import com.change_vision.jude.api.inf.model.IRealization;
import com.change_vision.jude.api.inf.presentation.INodePresentation;
import com.change_vision.jude.api.inf.project.ProjectAccessor;
import com.change_vision.jude.api.inf.AstahAPI;


/**
 * Sample source codes for creating a Class Diagram and presentations by Astah API.
 * Create models of a package, classes and relations, and them create a class diagram
 * and other presentations. 
 */
public class APIForCreatingPresentationsSample {
	
	//installer
	private static final String PROJECT_PATH = "./SampleModel.asta";
	//eclipse
	//private static final String PROJECT_PATH = "api_sample\\simpleDgmEdit\\SampleModel.asta";
	
	public static void main(String[] args) {
		
		try {
			System.out.println("Creating new project...");
			
            // Create a project and get a root model
			ProjectAccessor prjAccessor = AstahAPI.getAstahAPI().getProjectAccessor();
			prjAccessor.create(PROJECT_PATH);
			IModel project = prjAccessor.getProject();
			
			System.out.println("Creating new elements in the project...");
			
			// Begin transaction when creating or editing models
			TransactionManager.beginTransaction();
			
			// -----<<< Create Model >>>-----
			BasicModelEditor basicModelEditor = ModelEditorFactory.getBasicModelEditor();
			
			// Create a package
			IPackage packageA = basicModelEditor.createPackage(project, "PackageA");
			
			// Create a class in the specified package
			IClass classA = basicModelEditor.createClass(packageA, "ClassA");
			// Add an attribute to the class
			basicModelEditor.createAttribute(classA, "attribute0", "int");
			// Add an operation to the class
			basicModelEditor.createOperation(classA, "operation0", "void");
			
			// Create classes and an interface in the specified package
			IClass classB = basicModelEditor.createClass(packageA, "ClassB");
			IClass classC = basicModelEditor.createClass(packageA, "ClassC");
			IClass interfaceA = basicModelEditor.createInterface(packageA, "InterfaceA");
			
			// Add an association between classes
			IAssociation association 
				= basicModelEditor.createAssociation(classA, classB, "association name",
					"classA end", "classB end");
			// Add a generalization between classes
			IGeneralization generalization 
				= basicModelEditor.createGeneralization(classC, classA, "generalization name");
			// Add a realization between a classe and an interface
			IRealization realization 
				= basicModelEditor.createRealization(classB, interfaceA, "realization name");
			
			// -----<<< Create Diagram >>>-----
			ClassDiagramEditor diagramEditor 
				= AstahAPI.getAstahAPI().getProjectAccessor().getDiagramEditorFactory().getClassDiagramEditor();

			// Create a class diagram in the specified package
			diagramEditor.createClassDiagram(packageA, "Class DiagramA");

			// Create class and interface presentations in the class diagram
			Point2D locationA = new Point2D.Double(10.0d, 10.0d);
			INodePresentation classAPs = diagramEditor.createNodePresentation(classA, locationA);
			
			Point2D locationB = new Point2D.Double(300.0d, 25.0d);
			INodePresentation classBPs = diagramEditor.createNodePresentation(classB, locationB);
			
			Point2D locationC = new Point2D.Double(45.0d, 200.0d);
			INodePresentation classCPs = diagramEditor.createNodePresentation(classC, locationC);
			
			Point2D locationD = new Point2D.Double(500.0d, 29.0d);
			INodePresentation interfaceAPs = diagramEditor.createNodePresentation(interfaceA, locationD);
						
			// Add an association presentation in the class diagram
			diagramEditor.createLinkPresentation(association, classAPs, classBPs);
			// Add a generalization presentation in the class diagram
			diagramEditor.createLinkPresentation(generalization, classAPs, classCPs);
			// Add a realization in the class diagram
			diagramEditor.createLinkPresentation(realization, interfaceAPs, classBPs);
			
			// Add color to a class presentation
			classAPs.setProperty("fill.color", "#FF0000");
			
			// End transaction
			TransactionManager.endTransaction();
			
			// Save project
			prjAccessor.save();
			
			// Close project
			prjAccessor.close();
			
			System.out.println("Finished");
			
		} catch (LicenseNotFoundException e) {
			e.printStackTrace();
		} catch (ProjectNotFoundException e) {
			e.printStackTrace();
		} catch (ProjectLockedException e) {
			e.printStackTrace();
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		} catch (InvalidEditingException e) {
			// Abort transaction
			TransactionManager.abortTransaction();
			// Get an exception message
			System.err.print(e.getMessage());
			e.printStackTrace();
		} catch (IOException e ) {
			e.printStackTrace();
		} catch (Throwable e) {
			e.printStackTrace();
		}
	}
}