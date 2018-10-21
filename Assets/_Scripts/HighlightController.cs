using UnityEngine;

public class HighlightController : MonoBehaviour {
  private UIPiece highlightObject;
  private UICasa highlightObject2;

  public void SelectObject(UIPiece highlightObject) {
    /*if (this.highlightObject != null) {
      this.highlightObject.StopHighlight();
    }*/

    this.highlightObject = highlightObject;
    this.highlightObject.StartHighlight();
  }

  public void NoSelectObject(UIPiece highlightObject) {
	this.highlightObject.StopHighlight();
  }

  public void SelectCasa(UICasa highlightObject2) {
    /*if (this.highlightObject2 != null) {
      this.highlightObject2.StopHighlight();
    }*/

    this.highlightObject2 = highlightObject2;
    this.highlightObject2.StartHighlight();
  }

  public void NoSelectCasa(UICasa highlightObject2) {
	this.highlightObject2.StopHighlight();
  }
}
