using UnityEngine;
using System.Collections;

public class fourDigitHexDisplay : MonoBehaviour {
    public Material onMat;
    public Material offMat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void setValue(int val) {
        int temp = val;
        if (val > 9999) {
            temp = 9999;
        }
        if (val > 999) {
            setDigit(transform.GetChild(0), temp / 1000);
            temp -= 1000 * (temp / 1000);
        } else {
            setDigit(transform.GetChild(0), 10);
        }
        if (val > 99) {
            setDigit(transform.GetChild(1), temp / 100);
            temp -= 100 * (temp / 100);
        } else {
            setDigit(transform.GetChild(1), 10);
        }
        if (val > 9) {
            setDigit(transform.GetChild(2), temp / 10);
            temp -= 10 * (temp / 10);
        } else {
            setDigit(transform.GetChild(2), 10);
        }
        setDigit(transform.GetChild(3), temp);
    }

    void setDigit(Transform digit, int number) {
        switch (number) {
            case 0:
                digit.transform.GetChild(0).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(1).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(2).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(3).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(4).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(5).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(6).GetComponent<Renderer>().material = offMat;
                break;
            case 1:
                digit.transform.GetChild(0).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(1).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(2).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(3).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(4).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(5).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(6).GetComponent<Renderer>().material = offMat;
                break;
            case 2:
                digit.transform.GetChild(0).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(1).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(2).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(3).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(4).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(5).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(6).GetComponent<Renderer>().material = onMat;
                break;
            case 3:
                digit.transform.GetChild(0).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(1).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(2).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(3).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(4).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(5).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(6).GetComponent<Renderer>().material = onMat;
                break;
            case 4:
                digit.transform.GetChild(0).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(1).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(2).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(3).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(4).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(5).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(6).GetComponent<Renderer>().material = onMat;
                break;
            case 5:
                digit.transform.GetChild(0).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(1).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(2).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(3).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(4).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(5).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(6).GetComponent<Renderer>().material = onMat;
                break;
            case 6:
                digit.transform.GetChild(0).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(1).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(2).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(3).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(4).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(5).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(6).GetComponent<Renderer>().material = onMat;
                break;
            case 7:
                digit.transform.GetChild(0).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(1).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(2).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(3).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(4).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(5).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(6).GetComponent<Renderer>().material = offMat;
                break;
            case 8:
                digit.transform.GetChild(0).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(1).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(2).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(3).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(4).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(5).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(6).GetComponent<Renderer>().material = onMat;
                break;
            case 9:
                digit.transform.GetChild(0).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(1).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(2).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(3).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(4).GetComponent<Renderer>().material = onMat;
                digit.transform.GetChild(5).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(6).GetComponent<Renderer>().material = onMat;
                break;
            case 10: //Alternate Zero so that 0001 is just 1
                digit.transform.GetChild(0).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(1).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(2).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(3).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(4).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(5).GetComponent<Renderer>().material = offMat;
                digit.transform.GetChild(6).GetComponent<Renderer>().material = offMat;
                break;
        }
    }
}
