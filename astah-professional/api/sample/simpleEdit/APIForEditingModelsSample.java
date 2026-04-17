import java.io.IOException;

import com.change_vision.jude.api.inf.AstahAPI;
import com.change_vision.jude.api.inf.editor.BasicModelEditor;
import com.change_vision.jude.api.inf.editor.ModelEditorFactory;
import com.change_vision.jude.api.inf.editor.TransactionManager;
import com.change_vision.jude.api.inf.exception.InvalidEditingException;
import com.change_vision.jude.api.inf.exception.LicenseNotFoundException;
import com.change_vision.jude.api.inf.exception.ProjectLockedException;
import com.change_vision.jude.api.inf.exception.ProjectNotFoundException;
import com.change_vision.jude.api.inf.model.IClass;
import com.change_vision.jude.api.inf.model.IModel;
import com.change_vision.jude.api.inf.model.IPackage;
import com.change_vision.jude.api.inf.project.ProjectAccessor;

/**
 * Sample source codes for creating Astah model by Astah API. 
 * Crate a package and two classes, then add an association between classes.
 * A class diagram is not generated in this sample.
 * To create a class diagram, please use [Auto caret class diagram] function in Astah.
 * Or, models in the structure tree in Astah can be dragged to a diagram.
 */
public class APIForEditingModelsSample {

	//installer
	private static final String PROJECT_PATH = "./SampleModel.asta";
	//eclipse
	//private static final String PROJECT_PATH = "api_sample\\simpleEdit\\SampleModel.asta";
	
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

            // Get model editor to create models in a class diagram
            BasicModelEditor basicModelEditor = ModelEditorFactory.getBasicModelEditor();

            // Create a package
            IPackage packageA = basicModelEditor.createPackage(project, "PackageA");

            // Create a class in the specified package
            IClass classA = basicModelEditor.createClass(packageA, "ClassA");
            // Add a definition to the class
            classA.setDefinition("Definition of ClassA");
            // Add an attribute to the class
            basicModelEditor.createAttribute(classA, "attribute0", "int");
            // Add an operation to the class
            basicModelEditor.createOperation(classA, "operation0", "void");

            // Create a class in the specified package
            IClass classB = basicModelEditor.createClass(packageA, "ClassB");

            // Add an association between classes
            basicModelEditor.createAssociation(classA, classB, "association name",
                    "classA end", "classB end");

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
            System.err.println(e.getMessage());
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (Throwable e) {
            e.printStackTrace();
        }
    }
}
