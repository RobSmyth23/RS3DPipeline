using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class graphicsPipeline : MonoBehaviour
{
    public GameObject screen;
    Renderer myRenderer;
    Texture2D texture;
    public Texture2D texture_file;
    public Color lineColor = Color.black;

    Model myModel;

    private StreamWriter writer;
    string filename = "Data.txt";
    private float angle;

    void Start()
    {
        writer = new StreamWriter(filename, false);

        myModel = new Model();

        List<Vector3> verts3 = myModel.vertices;
        List<Vector4> verts = convertToHomg(verts3);

        writeVectorsToFile(verts, "Vertices of model", " ---------- ");

        Vector3 axis = (new Vector3(9, 4, 4)).normalized;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(26, axis), Vector3.one);

        writeMatrixToFile(rotationMatrix, "Rotation Matrix", " ----------- ");

        List<Vector4> afterRot = applyTransformation(verts, rotationMatrix);
        writeVectorsToFile(afterRot, "Verts after rotation", " --------------- ");

        Matrix4x4 scaleMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(9, 1, 4));
        writeMatrixToFile(scaleMatrix, "Scale Matrix", " --------------- ");

        List<Vector4> afterScale = applyTransformation(afterRot, scaleMatrix);
        writeVectorsToFile(afterScale, "After Scale and Rotation", " ----------------- ");

        Matrix4x4 translationMatrix = Matrix4x4.TRS(new Vector3(1, 2, 5), Quaternion.identity, Vector3.one);
        writeMatrixToFile(translationMatrix, "Translation Matrix", " --------------- ");

        List<Vector4> afterTranslation = applyTransformation(afterScale, translationMatrix);
        writeVectorsToFile(afterTranslation, "After translation", " ----------------- ");

        Matrix4x4 mot = translationMatrix * scaleMatrix * rotationMatrix;
        writeMatrixToFile(mot, "Matrix of transforms", " ----------------- ");

        List<Vector4> afterAll = applyTransformation(verts, mot);
        writeVectorsToFile(afterAll, "Image After MoT", " ----------------- ");

        Matrix4x4 view = Matrix4x4.LookAt(new Vector3(11, 7, 54), new Vector3(4, 9, 4), new Vector3(5, 4, 9));
        writeMatrixToFile(view, "Viewing Matrix", " ---------- ");

        Matrix4x4 proj = Matrix4x4.Perspective(90, 1, 1, 1000);
        writeMatrixToFile(proj, "Projection Matrix", " ---------- ");

        writer.Close();

        var tests = new (Vector2 Start, Vector2 End, string Label)[]
        {
            (new Vector2(0, 0), new Vector2(2, 0), "Cross Right Edge"),
            (new Vector2(0, 0), new Vector2(-2, 0), "Cross Left Edge"),
            (new Vector2(0, 2), new Vector2(0, -2), "Cross Top & Bottom"),
            (new Vector2(2, 2), new Vector2(-2, -2), "Diagonal"),
            (new Vector2(0, 2), new Vector2(2, 2), "Horizontal"),
            (new Vector2(2, 0), new Vector2(2, -2), "Vertical"),
            (new Vector2(-2, 2), new Vector2(2, -2), "Corner to Corner"),
            (new Vector2(2, 4), new Vector2(4, -8), "Extended")
        };

        for (int i = 0; i < tests.Length; i++)
        {
            var (s, e, label) = tests[i];

            Debug.Log($"--- Test Case {i}: {label} ---");
            Debug.Log($"Intercept (Top): {Intercept(s, e, 0)}");

            if (LineClip(ref s, ref e))
                Debug.Log($"Clipped Line: Start={s}, End={e}");
            else
                Debug.Log("Line Rejected");

            Debug.Log("");
        }
    }

    private void writeVectorsToFile(List<Vector4> verts, string before, string after)
    {
        writer.WriteLine(before);
        foreach (Vector4 v in verts)
            writer.WriteLine($"({v.x},{v.y},{v.z},{v.w})");
        writer.WriteLine(after);
    }

    private void writeMatrixToFile(Matrix4x4 m, string before, string after)
    {
        writer.WriteLine(before);
        for (int i = 0; i < 4; i++)
        {
            Vector4 r = m.GetRow(i);
            writer.WriteLine($"({r.x},{r.y},{r.z},{r.w})");
        }
        writer.WriteLine(after);
    }

    private List<Vector4> convertToHomg(List<Vector3> verts)
    {
        List<Vector4> output = new List<Vector4>();
        foreach (Vector3 v in verts)
            output.Add(new Vector4(v.x, v.y, v.z, 1));
        return output;
    }

    private List<Vector4> applyTransformation(List<Vector4> verts, Matrix4x4 m)
    {
        List<Vector4> output = new List<Vector4>();
        foreach (Vector4 v in verts)
            output.Add(m * v);
        return output;
    }

    private List<Vector2> projection(List<Vector4> pts)
    {
        List<Vector2> output = new List<Vector2>();
        foreach (var p in pts)
            output.Add(new Vector2(p.x / p.z, p.y / p.z));
        return output;
    }

    private Vector2Int convertToPixel(Vector2 p, int width, int height)
    {
        return new Vector2Int(
            (int)((p.x + 1) * (width - 1) / 2),
            (int)((p.y + 1) * (height - 1) / 2)
        );
    }

    private List<Vector2Int> convertToPixels(List<Vector2> pts, int w, int h)
    {
        List<Vector2Int> result = new List<Vector2Int>();
        foreach (var p in pts)
            result.Add(convertToPixel(p, w, h));
        return result;
    }

    bool LineClip(ref Vector2 start, ref Vector2 end)
    {
        Outcode sCode = new Outcode(start);
        Outcode eCode = new Outcode(end);
        Outcode inside = new Outcode();

        if ((sCode + eCode) == inside)
            return true;

        if ((sCode * eCode) != inside)
            return false;

        if (sCode == inside)
            return LineClip(ref end, ref start);

        if (sCode.up)
        {
            start = Intercept(start, end, 0);
            return LineClip(ref start, ref end);
        }
        if (sCode.down)
        {
            start = Intercept(start, end, 1);
            return LineClip(ref start, ref end);
        }
        if (sCode.left)
        {
            start = Intercept(start, end, 2);
            return LineClip(ref start, ref end);
        }
        if (sCode.right)
        {
            start = Intercept(start, end, 3);
            return LineClip(ref start, ref end);
        }

        return false;
    }

    Vector2 Intercept(Vector2 start, Vector2 end, int edgeIndex)
    {
        if (end.x != start.x)
        {
            float m = (end.y - start.y) / (end.x - start.x);

            switch (edgeIndex)
            {
                case 0:
                    if (m != 0)
                        return new Vector2(start.x + (1f / m) * (1 - start.y), 1);
                    return new Vector2(start.x, 1);

                case 1:
                    if (m != 0)
                        return new Vector2(start.x + (1f / m) * (-1 - start.y), -1);
                    return new Vector2(start.x, -1);

                case 2:
                    return new Vector2(-1, start.y + m * (-1 - start.x));

                default:
                    return new Vector2(1, start.y + m * (1 - start.x));
            }
        }
        else
        {
            switch (edgeIndex)
            {
                case 0: return new Vector2(start.x, 1);
                case 1: return new Vector2(start.x, -1);
                case 2: return new Vector2(-1, start.y);
                default: return new Vector2(1, start.y);
            }
        }
    }

    List<Vector2Int> bresenham(Vector2Int start, Vector2Int end)
    {
        int dx = end.x - start.x;

        if (dx < 0)
            return bresenham(end, start);

        int dy = end.y - start.y;

        if (dy < 0)
            return NegY(bresenham(NegY(start), NegY(end)));

        if (dy > dx)
            return SwapXY(bresenham(SwapXY(start), SwapXY(end)));

        int dyMinusDx = dy - dx;
        int neg = 2 * dyMinusDx;
        int pos = 2 * dy;
        int p = 2 * dy - dx;

        List<Vector2Int> points = new List<Vector2Int>();
        int y = start.y;

        points.Add(start);

        for (int x = start.x + 1; x <= end.x; x++)
        {
            if (p < 0)
            {
                p += pos;
            }
            else
            {
                y++;
                p += neg;
            }
            points.Add(new Vector2Int(x, y));
        }

        return points;
    }

    private Vector2Int SwapXY(Vector2Int v) => new Vector2Int(v.y, v.x);
    private List<Vector2Int> SwapXY(List<Vector2Int> list)
    {
        List<Vector2Int> r = new List<Vector2Int>();
        foreach (var v in list) r.Add(SwapXY(v));
        return r;
    }

    private Vector2Int NegY(Vector2Int v) => new Vector2Int(v.x, -v.y);
    private List<Vector2Int> NegY(List<Vector2Int> list)
    {
        List<Vector2Int> r = new List<Vector2Int>();
        foreach (var v in list) r.Add(NegY(v));
        return r;
    }

    void Update()
    {
        texture = new Texture2D(512, 512);
        angle += 8f;

        List<Vector4> verts = convertToHomg(myModel.vertices);

        Matrix4x4 rotation = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(angle, Vector3.up), Vector3.one);
        Matrix4x4 translation = Matrix4x4.TRS(new Vector3(0, 0, 5), Quaternion.identity, Vector3.one);

        Matrix4x4 world = rotation * translation;
        Matrix4x4 view = Matrix4x4.LookAt(new Vector3(0, 0, 10), Vector3.zero, Vector3.up);
        Matrix4x4 proj = Matrix4x4.Perspective(90, 1, 1, 1000);

        Matrix4x4 mvp = proj * view * world;

        List<Vector4> ptsProj = applyTransformation(verts, mvp);
        List<Vector2> viewportPts = projection(ptsProj);

        for (int i = 0; i < myModel.faces.Count; i++)
        {
            //draw the lines
            Vector3Int face = myModel.faces[i];
            Vector2 p1 = viewportPts[face.x];
            Vector2 p2 = viewportPts[face.y];
            Vector2 p3 = viewportPts[face.z];

            Vector2 a = p2 - p1;
            Vector2 b = p3 - p2;

            if (Vector3.Cross(a, b).z < 0)
            {
               // textureTriangle(i, verts, texture_file, texture);
               Dictionary<int, RangeX> dict = new Dictionary<int, RangeX>();

                drawLine(p1, p2, texture, Color.blue, ref dict);
                drawLine(p2, p3, texture, Color.blue, ref dict);
                drawLine(p3, p1, texture, Color.blue, ref dict);

                foreach (var item in dict)
                {
                    for (int x = item.Value.start; x <= item.Value.end; x++)
                    {
                        texture.SetPixel(x, item.Key, Color.darkGreen);
                    }
 }






            }
        }
        myRenderer = screen.GetComponent<Renderer>();
        texture.Apply();
        myRenderer.material.mainTexture = texture;
    }

    private void drawLine(Vector2 v1, Vector2 v2, Texture2D texture, Color col, ref Dictionary<int, RangeX> dict)
    {
        Vector2 s = v1;
        Vector2 e = v2;
        //to ensure the lines dont warp and go outside the screen, i.e. clip
        if (LineClip(ref s, ref e))
        {
            List<Vector2Int> pts = bresenham(convertToPixel(s, texture.width, texture.height),
                                             convertToPixel(e, texture.width, texture.height));

            foreach (var p in pts)
            {
                texture.SetPixel(p.x, p.y, col);
                if (dict.ContainsKey(p.y))
                {
                    dict[p.y].AddPoint(p.x);
                }
                else
                {
                    dict[p.y] = new RangeX();
                    dict[p.y].AddPoint(p.x);
                }
                //for(y in dict), add rang.x to it and point p.y
            }
        }
    }
    private void textureTriangle(int i, List<Vector4> verts, Texture2D textureFile, Texture2D target)
    {
        Vector3Int face = myModel.faces[i];
        Vector3Int texIdx = myModel.texture_index_list[i];

        // UVs
        Vector2 a_t = myModel.texture_coordinates[texIdx.x];
        Vector2 b_t = myModel.texture_coordinates[texIdx.y];
        Vector2 c_t = myModel.texture_coordinates[texIdx.z];

        // Vertices
        Vector3 a = verts[face.x];
        Vector3 b = verts[face.y];
        Vector3 c = verts[face.z];

        // Project to screen
        Vector2 aProj = projection(new List<Vector4> { new Vector4(a.x, a.y, a.z, 1) })[0];
        Vector2 bProj = projection(new List<Vector4> { new Vector4(b.x, b.y, b.z, 1) })[0];
        Vector2 cProj = projection(new List<Vector4> { new Vector4(c.x, c.y, c.z, 1) })[0];

        Vector2Int a2 = convertToPixel(aProj, texture.width, texture.height);
        Vector2Int b2 = convertToPixel(bProj, texture.width, texture.height);
        Vector2Int c2 = convertToPixel(cProj, texture.width, texture.height);

        // Bounding box in Y
        int minY = Mathf.FloorToInt(Mathf.Min(a2.y, Mathf.Min(b2.y, c2.y)));
        int maxY = Mathf.CeilToInt(Mathf.Max(a2.y, Mathf.Max(b2.y, c2.y)));

        // Loop scanlines
        for (int y_p = minY; y_p <= maxY; y_p++)
        {
            RangeX range = new RangeX();

            // Add intersections with edges
            if ((a2.y <= y_p && b2.y >= y_p) || (b2.y <= y_p && a2.y >= y_p))
            {
                float t = (y_p - a2.y) / (b2.y - a2.y);
                int x = Mathf.RoundToInt(a2.x + t * (b2.x - a2.x));
                range.AddPoint(x);
            }
            if ((b2.y <= y_p && c2.y >= y_p) || (c2.y <= y_p && b2.y >= y_p))
            {
                float t = (y_p - b2.y) / (c2.y - b2.y);
                int x = Mathf.RoundToInt(b2.x + t * (c2.x - b2.x));
                range.AddPoint(x);
            }
            if ((c2.y <= y_p && a2.y >= y_p) || (a2.y <= y_p && c2.y >= y_p))
            {
                float t = (y_p - c2.y) / (a2.y - c2.y);
                int x = Mathf.RoundToInt(c2.x + t * (a2.x - c2.x));
                range.AddPoint(x);
            }

            if (range.start != -1 && range.end != -1)
            {
                for (int x_p = range.start; x_p <= range.end; x_p++)
                {
                    // Your barycentric snippet
                    Vector2 A = b2 - a2;
                    Vector2 B = c2 - a2;
                    Vector2 A_t = b_t - a_t;
                    Vector2 B_t = c_t - a_t;

                    float x = x_p - a2.x;
                    float y = y_p - a2.y;

                    float denom = (A.x * B.y - A.y * B.x);
                    if (Mathf.Abs(denom) < 1e-6f) continue;

                    float r = (x * B.y - y * B.x) / denom;
                    float s = (A.x * y - x * A.y) / denom;

                    if (r >= 0 && s >= 0 && r + s <= 1)
                    {
                        Vector2 texture_point = a_t + r * A_t + s * B_t;
                        texture_point.x *= textureFile.width;
                        texture_point.y *= textureFile.height;

                        Color color = textureFile.GetPixel((int)texture_point.x, (int)texture_point.y);
                        target.SetPixel(x_p, y_p, color);
                    }
                }
            }
        }
    }

}
