using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DailyJob3D : MonoBehaviour {

    Vector3[,] firstPossibilities, secondPossibilities, thirdPossibilities, fourthPossibilities, fifthPossibilities;
    Vector3[,] startPossibilities, endPossibilities;
    Transform startPos;
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject parent;
    [SerializeField]
    float width;
    [SerializeField]
    float height;
    [SerializeField]
    int maxAmount;
    [SerializeField]
    GameObject go;

    List<Vector3[,]> arrayList;

    int linePoints = 0;
    //[SerializeField]
    //LineRenderer lineRender;
    [SerializeField]
    LineRenderer lineRender;
    float inScreenSizeX;
    float inScreenSizeY;
    int maxAmountFixed;
    [SerializeField]
    int collumns;

    // Use this for initialization
    void Start() {
        arrayList = new List<Vector3[,]>();
        inScreenSizeX = transform.localScale.x;
        inScreenSizeY = transform.localScale.y;

        height = inScreenSizeY / (maxAmount * collumns + 4.0f);
        width = inScreenSizeX / maxAmount;
        maxAmountFixed = Mathf.CeilToInt(maxAmount / 2.0f);

        for (int i = 0; i < collumns; i++) {

            arrayList.Add(new Vector3[maxAmount, maxAmount]);
        }
        arrayList.Insert(0, new Vector3[maxAmount, 1]);
        arrayList.Insert(arrayList.Count, new Vector3[maxAmount, 1]);

        lineRender.positionCount = maxAmount + 2;
        startPos = transform;
        if (collumns % 2 == 1) {
            for (int x = -maxAmountFixed + 1; x < maxAmountFixed; x++) {
                int xFixed = x + maxAmountFixed - 1;
                for (int y = -maxAmountFixed + 1; y < maxAmountFixed; y++) {
                    int yFixed = y + maxAmountFixed - 1;
                    Vector3[,] temp;
                    if (y == -maxAmountFixed + 1) {
                        temp = arrayList[0];
                        temp[xFixed, yFixed] = new Vector3(transform.position.x + x * width, transform.position.y + yFixed * height + (-maxAmountFixed) * maxAmount * height, transform.position.z);
                        arrayList[0] = temp;
                        temp = arrayList[arrayList.Count - 1];
                        temp[xFixed, yFixed] = new Vector3(transform.position.x + x * width, transform.position.y + yFixed * height + (maxAmountFixed) * maxAmount * height - height, transform.position.z);
                        arrayList[arrayList.Count - 1] = temp;
                    }
                    for (int i = 1; i < arrayList.Count - 1; i++) {

                        temp = arrayList[i];
                        temp[xFixed, yFixed] = new Vector3(transform.position.x + x * width, transform.position.y + yFixed * height + (-maxAmountFixed + i - 1) * maxAmount * height + 2 * height, transform.position.z);
                        arrayList[i] = temp;
                    }
                }
            }
        } else if (collumns % 2 == 0) {
            for (int x = -maxAmountFixed; x < maxAmountFixed; x++) {
                int xFixed = x + maxAmountFixed;
                for (int y = -maxAmountFixed; y < maxAmountFixed; y++) {
                    int yFixed = y + maxAmountFixed;
                    Vector3[,] temp;
                    if (y == -maxAmountFixed) {
                        temp = arrayList[0];
                        temp[xFixed, yFixed] = new Vector3(transform.position.x + x * width + width / 2, transform.position.y + yFixed * height + (-maxAmountFixed + 0) * maxAmount * height - 2 * height + height / 2, transform.position.z);
                        arrayList[0] = temp;
                        temp = arrayList[arrayList.Count - 1];
                        temp[xFixed, yFixed] = new Vector3(transform.position.x + x * width + width / 2, transform.position.y + yFixed * height + (-maxAmountFixed + 4) * maxAmount * height + 2 * height + height / 2, transform.position.z);
                        arrayList[arrayList.Count - 1] = temp;
                    }
                    for (int i = 1; i < arrayList.Count - 1; i++) {
                        temp = arrayList[i];
                        temp[xFixed, yFixed] = new Vector3(transform.position.x + x * width + width / 2, transform.position.y + yFixed * height + (-maxAmountFixed + i) * maxAmount * height + height / 2, transform.position.z);
                        arrayList[i] = temp;
                    }
                }
            }
        }
        for (int i = 0; i < arrayList.Count; i++) {
            Randomizer(arrayList[i]);
        }
        float lineLength = 0.0f;
        for (int i = 1; i < lineRender.positionCount; i++) {
            lineLength += (lineRender.GetPosition(i) - lineRender.GetPosition(i - 1)).magnitude;
        }
    }

    // Update is called once per frame
    void Update() {

    }

    void Randomizer(Vector3[,] array) {
        int randX = 0;
        int randY = 0;
        if (array.Length < 16) {
            randX = Random.Range(0, maxAmount);
            randY = Random.Range(0, 1);
        } else {
            randX = Random.Range(0, maxAmount);
            randY = Random.Range(0, maxAmount);
        }
        GameObject go = Instantiate(prefab, array[randX, randY], Quaternion.identity, parent.transform);
        lineRender.SetPosition(linePoints, go.transform.position);
        linePoints++;
    }

    void DrawLine() {

    }
}