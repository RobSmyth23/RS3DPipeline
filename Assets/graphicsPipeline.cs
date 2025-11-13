using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class graphicsPipeline : MonoBehaviour
{
    StreamWriter writer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        writer = new StreamWriter("Data.txt", false);

        Model mymodel = new Model();

        List<Vector3> verts3 = mymodel.vertices;
        List<Vector4> verts = convertToHomg(verts3);
        writeVectorsToFile(verts, "Vertices of my letter ", " ---------- ");


        Vector3 axis = (new Vector3(9, 4, 4)).normalized;

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero,
            Quaternion.AngleAxis(26, axis),
            Vector3.one);

        writeMatrixToFile(rotationMatrix, "Rotation Matrix", " ----------- ");

        List<Vector4> imageAfterRotation = applyTransformation(verts, rotationMatrix);

        writeVectorsToFile(imageAfterRotation, "Verts after rotation ", " --------------- ");

        Matrix4x4 scaleMatrix = Matrix4x4.TRS(Vector3.zero,
            Quaternion.identity,
            new Vector3(9, 1, 4));

        writeMatrixToFile(scaleMatrix, "Scale Matrix ", " --------------- ");

        List<Vector4> imageAfterScale = applyTransformation(imageAfterRotation, scaleMatrix);

        writeVectorsToFile(imageAfterScale, " After Scale and Rotation ", " ----------------- ");

        Matrix4x4 translationMatrix = Matrix4x4.TRS(new Vector3(1, 2, 5),
            Quaternion.identity,
            new Vector3(1, 1, 1));

        writeMatrixToFile(translationMatrix, "translation Matrix ", " --------------- ");

        List<Vector4> imageAftertranslation = applyTransformation(imageAfterScale, translationMatrix);

        writeVectorsToFile(imageAftertranslation, " After translation ", " ----------------- ");

        Matrix4x4 MoT = translationMatrix * scaleMatrix * rotationMatrix;

        writeMatrixToFile(MoT, "Matrix of transformations: ", " --------------- ");

        List<Vector4> imageAfterMoT = applyTransformation(verts, MoT);

        writeVectorsToFile(imageAfterMoT, " Image After MoT ", " ----------------- ");

        //    --------------------------------------------------------
        //#########################################################


        Matrix4x4 viewingMatrix = Matrix4x4.LookAt(new Vector3(11, 7, 54), new Vector3(4, 9, 4), new Vector3(5, 4, 9));

        //translation

        writeMatrixToFile(viewingMatrix, " Viewing Matrix ", " --------------- ");

        Matrix4x4 projection = Matrix4x4.Perspective(90, 1, 1, 1000);

        writeMatrixToFile(projection, " Projection ", " ------------ ");

        writer.Close();


        var tests = new (Vector2 Start, Vector2 End, string Label)[]
        {
            //(new Vector2(0, 0), new Vector2(0.5f, 0.5f), "Trivial Accept"),
           // (new Vector2(2, 2), new Vector2(3, 3), "Trivial Reject"),
            (new Vector2(0, 0), new Vector2(2, 0), "Cross Right Edge"),
            (new Vector2(0, 0), new Vector2(-2, 0), "Cross Left Edge"),
            (new Vector2(0, 2), new Vector2(0, -2), "Cross Top & Bottom"),
            (new Vector2(2, 2), new Vector2(-2, -2), "Diagonal Across Viewport"),
            (new Vector2(0, 2), new Vector2(2, 2), "Horizontal Across Viewport"),
            (new Vector2(2, 0), new Vector2(2, -2), "Vertical Across Viewport"),
            (new Vector2(-2, 2), new Vector2(2, -2), "Corner to Corner"),
            (new Vector2(2, 4), new Vector2(4, -8), "Extended Example")
        };

        for (int i = 0; i < tests.Length; i++)
        {
            var (start, end, label) = tests[i];

            print($"--- Test Case {i}: {label} ---");
            print($"Intercept (Top Edge): {Intercept(start, end, 0)}");

            if (LineClip(ref start, ref end))
            {
                print($"Clipped Line: Start={start}, End={end}");
            }
            else
            {
                print("Line Rejected");
            }

            print("");

        }
    }

    private void writeVectorsToFile(List<Vector4> verts, string before, string after)
    {
        writer.WriteLine(before);

        foreach (Vector4 v in verts)
        {
            writer.WriteLine(" ( " + v.x + " , " + v.y + " , " + v.z + " , " + v.w + " ) ");
        }
        writer.WriteLine(after);
    }



    private void writeMatrixToFile(Matrix4x4 matrix, string before, string after)
    {
        writer.WriteLine(before);

        for (int i = 0; i < 4; i++)
        {
            Vector4 v = matrix.GetRow(i);
            writer.WriteLine(" ( " + v.x + " , " + v.y + " , " + v.z + " , " + v.w + " ) ");
        }
        writer.WriteLine(after);
    }

    private List<Vector4> convertToHomg(List<Vector3> vertices)
    {
        List<Vector4> output = new List<Vector4>();

        foreach (Vector3 v in vertices)
        {
            output.Add(new Vector4(v.x, v.y, v.z, 1.0f));

        }
        return output;

    }

    private List<Vector4> applyTransformation
        (List<Vector4> verts, Matrix4x4 tranformMatrix)
    {
        List<Vector4> output = new List<Vector4>();
        foreach (Vector4 v in verts)
        { output.Add(tranformMatrix * v); }

        return output;

    }

    private void displayMatrix(Matrix4x4 rotationMatrix)
    {
        for (int i = 0; i < 4; i++)
        { print(rotationMatrix.GetRow(i)); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool LineClip(ref Vector2 start, ref Vector2 end)
    {
        Outcode startCode = new Outcode(start);
        Outcode endCode = new Outcode(end);
        Outcode inViewPort = new Outcode();

        //check for trivial accept
        if ((startCode + endCode) == inViewPort)
            return true;

        //test for trivial rejection
        if ((startCode * endCode) != inViewPort)
            return false;

        //we want the start of the line to begin where the viewport starts,
        //so we change the start to the point where the viewport intercepts it.

        if (startCode == inViewPort)
        {
            return LineClip(ref end, ref start);
        }
        
        if (startCode.up)
        {
            start = Intercept(start, end, 0);
            return LineClip(ref start, ref end);
        }

        if (startCode.down)
        {
            start = Intercept(start, end, -1);
            return LineClip(ref start, ref end);
        }
        if (startCode.left)
        {
            start = Intercept(start, end, 2);
            return LineClip(ref start, ref end);
        }
        if (startCode.right)
        {
            start = Intercept(start, end, 3);
            return LineClip(ref start, ref end);
        }

        return false;
    }
    //UDLR - UP DOWN LEFT RIGHT
    //1111 - 1   1    1    1
    //0000 - 0   0    0    0
    //therefore if udlr = 0100 it is 0d00 so its down from viewport
    // if the udlr = 1001, it is 1 above and 1 to the right of the viewport : therefore not in the viewport
   
    Vector2 Intercept(Vector2 start, Vector2 end, int edgeIndex)
    {
        if (end.x != start.x)
        {
            float m = (end.y - start.y) / (end.x - start.x);

            switch (edgeIndex)
            {
                case 0:
                    if (m != 0)
                    {
                        return new Vector2(start.x + (1 / m) * (1 - start.y), 1);
                    }
                    else
                    {
                        //this should never happen if called on outcode device
                        if (start.y == 1)
                            return start;
                        else
                            return new Vector2(float.NaN, float.NaN);
                    }

                    break;

                case 1:
                    if (m != 0)
                    {
                        return new Vector2(start.x + (1 / m) * (-1 - start.y), -1);
                    }
                    else
                    {
                        //this should never happen if called on outcode device
                        if (start.y == -1)
                            return start;
                        else
                            return new Vector2(float.NaN, float.NaN);
                    }


                case 2: // Left Edge x=-1 y = ?
                    // y = y1 + m(x - x1)

                    return new Vector2(1, start.y + m * (-1 - start.x));

                default:
                    // Right Edge x=1 y=?
                    // y = y1 + m(x - x1)
                    return new Vector2(1, start.y + m * (1 - start.x));
            }
        }
        else
        {
            // m = infinity i.e. a vertical line

            switch (edgeIndex)
            {
                case 0:
                    // Top Edge 
                    return new Vector2(start.x, 1);

                case 1:
                    // Bottom Edge
                    return new Vector2(start.x, -1);

                case 2:
                    // Left Edge  (This cannot occur if called on Outcode advice)
                    if (start.x == -1)
                        return start;

                    return new Vector2(float.NaN, float.NaN);
                
                default:
                    // Right Edge  (This cannot occur if called on Outcode advice)
                    if (start.x == 1)
                        return start;

                    return new Vector2(float.NaN, float.NaN);


            }
        }
       
    }
    
}
