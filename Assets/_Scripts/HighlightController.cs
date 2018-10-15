using UnityEngine;

public class HighlightController : MonoBehaviour {
  private UIPiece highlightObject;

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
}
