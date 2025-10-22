using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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


        Vector3 axis = (new Vector3(9,4,4)).normalized;

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
}
