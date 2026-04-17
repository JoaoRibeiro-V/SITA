import java.awt.geom.Point2D;
import java.io.IOException;

import com.change_vision.jude.api.inf.exception.InvalidUsingException;
import com.change_vision.jude.api.inf.exception.LicenseNotFoundException;
import com.change_vision.jude.api.inf.exception.NonCompatibleException;
import com.change_vision.jude.api.inf.exception.ProjectLockedException;
import com.change_vision.jude.api.inf.exception.ProjectNotFoundException;
import com.change_vision.jude.api.inf.model.IDiagram;
import com.change_vision.jude.api.inf.model.IModel;
import com.change_vision.jude.api.inf.presentation.IHeaderCell;
import com.change_vision.jude.api.inf.presentation.ILinkPresentation;
import com.change_vision.jude.api.inf.presentation.INodePresentation;
import com.change_vision.jude.api.inf.presentation.IPresentation;
import com.change_vision.jude.api.inf.presentation.IValueCell;
import com.change_vision.jude.api.inf.project.ProjectAccessor;
import com.change_vision.jude.api.inf.AstahAPI;

/**
 * Sample source codes for obtaining presentation information of Astah models. 
 * Output location, width, and height etc in console.
 */
public class APIForReadingDgmModelsSample {
	
	//installer
	private static final String PROJECT_PATH = "./SampleModel.asta";
	//eclipse
	//private static final String PROJECT_PATH = "api_sample\\simpleDgmRead\\SampleModel.asta";
	
    public static void main(String[] args) {
        try {
            System.out.println("Opening project...");

            ProjectAccessor prjAccessor = AstahAPI.getAstahAPI().getProjectAccessor();

            // open project
            prjAccessor.open(PROJECT_PATH, true, false, true);

            System.out.println("Printing the elements...");

            // get root model
            IModel project = prjAccessor.getProject();
            
            IDiagram[] diagrams = project.getDiagrams();
            
            for (int i = 0; i < diagrams.length; i++) {
            	//get diagram's presentation information
            	printDiagramInfo(diagrams[i]);
			}

            // closes project
            prjAccessor.close();

            System.out.println("Finished");

        } catch (LicenseNotFoundException e) {
            e.printStackTrace();
        } catch (ProjectNotFoundException e) {
            e.printStackTrace();
        } catch (ProjectLockedException e) {
            e.printStackTrace();
        } catch (NonCompatibleException e) {
            e.printStackTrace();
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (InvalidUsingException e) {
        	e.printStackTrace();
        } catch (Throwable e) {
            e.printStackTrace();
        }
    }

    private static void printDiagramInfo(IDiagram iDiagram) throws InvalidUsingException {
    	IPresentation[] presentations = iDiagram.getPresentations();
    	
    	StringBuilder dgmInfo = new StringBuilder("[Diagram] name=");
        dgmInfo.append(iDiagram.getName());
        dgmInfo.append(" definition=");
        dgmInfo.append(iDiagram.getDefinition());
        dgmInfo.append(" presentation's number=");
        dgmInfo.append(presentations.length);
        System.out.println(dgmInfo.toString());
        
        for (int i = 0; i < presentations.length; i++) {
        	IPresentation p = presentations[i];
        	// presentation information
            if (p instanceof INodePresentation) {
                printNodePresentationInfo((INodePresentation)p);
            } else if (p instanceof ILinkPresentation) {
            	printLinkPresentationInfo((ILinkPresentation)p);
            } else if (p instanceof IHeaderCell) {
            	printHeaderCellInfo((IHeaderCell)p);
            } else if (p instanceof IValueCell) {
            	printValueCellInfo((IValueCell)p);
            }
        }
    }

	private static void printNodePresentationInfo(INodePresentation p) {
		StringBuilder presentationInfo = new StringBuilder("  [NodePresentation] model=");
		presentationInfo.append(p.getModel());
        presentationInfo.append(" label=");
        presentationInfo.append(p.getLabel());
        presentationInfo.append(" type=");
        presentationInfo.append(p.getType());
        INodePresentation[] parents = p.getParents();
        for (int i = 0; i < parents.length; i++) {
            presentationInfo.append(" parents[");
            presentationInfo.append(i);
            presentationInfo.append("]=");
            presentationInfo.append(parents[i].getLabel());
		}
        INodePresentation[] children = p.getChildren();
        for (int i = 0; i < children.length; i++) {
            presentationInfo.append(" children[");
            presentationInfo.append(i);
            presentationInfo.append("]=");
            presentationInfo.append(children[i].getLabel());
		}
        ILinkPresentation[] links = p.getLinks();
        for (int i = 0; i < links.length; i++) {
            presentationInfo.append(" links[");
            presentationInfo.append(i);
            presentationInfo.append("]=");
            presentationInfo.append(links[i].getLabel());
		}
        presentationInfo.append(" location=");
        presentationInfo.append(p.getLocation());
        presentationInfo.append(" width=");
        presentationInfo.append(p.getWidth());
        presentationInfo.append(" height=");
        presentationInfo.append(p.getHeight());
        presentationInfo.append(" color=");
        presentationInfo.append(p.getProperty("fill.color"));
        System.out.println(presentationInfo.toString());
	}

	private static void printLinkPresentationInfo(ILinkPresentation p) {
		StringBuilder presentationInfo = new StringBuilder("  [LinkPresentation] model=");
		presentationInfo.append(p.getModel());
        presentationInfo.append(" label=");
        presentationInfo.append(p.getLabel());
        presentationInfo.append(" type=");
        presentationInfo.append(p.getType());
        presentationInfo.append(" source=");
        presentationInfo.append(p.getSource().getLabel());
        presentationInfo.append(" target=");
        presentationInfo.append(p.getTarget().getLabel());
        Point2D[] points = p.getPoints();
        for (int i = 0; i < points.length; i++) {
            presentationInfo.append(" points[");
            presentationInfo.append(i);
            presentationInfo.append("]=");
            presentationInfo.append(points[i]);
		}
        presentationInfo.append(" color=");
        presentationInfo.append(p.getProperty("line.color"));
        System.out.println(presentationInfo.toString());
	}

	private static void printHeaderCellInfo(IHeaderCell p) {
		StringBuilder presentationInfo = new StringBuilder("  [HeaderCell] model=");
		presentationInfo.append(p.getModel());
        presentationInfo.append(" label=");
        presentationInfo.append(p.getLabel());
        presentationInfo.append(" type=");
        presentationInfo.append(p.getType());
        presentationInfo.append(" visible=");
        presentationInfo.append(p.isVisible());
        presentationInfo.append(" total=");
        presentationInfo.append(p.isTotal());
        System.out.println(presentationInfo.toString());
	}
    
	private static void printValueCellInfo(IValueCell p) {
		StringBuilder presentationInfo = new StringBuilder("  [ValueCell] model=");
		presentationInfo.append(p.getModel());
        presentationInfo.append(" label=");
        presentationInfo.append(p.getLabel());
        presentationInfo.append(" type=");
        presentationInfo.append(p.getType());
        presentationInfo.append(" color=");
        String color = p.getProperty("fill.color");
        if (!color.startsWith("#")) {
        	IDiagram diagram = p.getDiagram();
        	color = diagram.getProperty("matrix.automatic_cell.color");
        }
        if (color.startsWith("#")) {
        	presentationInfo.append(color);
        }
        System.out.println(presentationInfo.toString());
	}
}
