using UnityEngine;
using System.Collections.Generic;
using System;

public class Model
{
	public List<Vector3> vertices;
    List<Vector2> texture_coordinates;
    List<Vector3Int> texture_index_list;
    List<Vector3> normals;

    public List<Vector3Int> faces;

	public Model()
	{
		defineVertices();
        defineUVs();
		defineFaces();
		CreateUnityGameObject();
	}

    private void defineUVs()
    {
        List<Vector2Int> uv_list = new List<Vector2Int>();
        uv_list.Add(new Vector2Int(105,399)); //0
        uv_list.Add(new Vector2Int(105, 883));//1
        uv_list.Add(new Vector2Int(206,467));//2
        uv_list.Add(new Vector2Int(206, 588));//3
        uv_list.Add(new Vector2Int(206,717));//4
        uv_list.Add(new Vector2Int(206,880));//5
        uv_list.Add(new Vector2Int(206,640));//6
        uv_list.Add(new Vector2Int(297,466));//7
        uv_list.Add(new Vector2Int(297,588));//8
        uv_list.Add(new Vector2Int(299,816));//9
        
        uv_list.Add(new Vector2Int(299,878));//10
        uv_list.Add(new Vector2Int(302,404));//11
        uv_list.Add(new Vector2Int(296,641));//12
        uv_list.Add(new Vector2Int(364,821));//13
        uv_list.Add(new Vector2Int(365,876));//14
        uv_list.Add(new Vector2Int(361,462));//15
        uv_list.Add(new Vector2Int(361,587));//16
        
        uv_list.Add(new Vector2Int(463,405));//17
        uv_list.Add(new Vector2Int(463,644));//18
        uv_list.Add(new Vector2Int(516,405));//19
        uv_list.Add(new Vector2Int(516,644));//20
        uv_list.Add(new Vector2Int(498,662));//21
        uv_list.Add(new Vector2Int(498,706));//22
        uv_list.Add(new Vector2Int(649,662));//23
        uv_list.Add(new Vector2Int(649,706));//24

        texture_coordinates = ConvertToRelative(uv_list);

    }

    private List<Vector2> ConvertToRelative(List<Vector2Int> uv_list)
    {
        List<Vector2> cooirs = new List<Vector2>();
        foreach (Vector2Int uv in uv_list)
        {
            cooirs.Add(new Vector2(uv.x / 1023f,1-  uv.y / 1023f));
        }

        return cooirs;
    }

    void defineVertices()
    {
        //front vertices
        vertices = new List<Vector3>();
        vertices.Add(new Vector3(-3f, 7f, 1f)); //0
        vertices.Add(new Vector3(-3f, -7f, 1f)); //1
        vertices.Add(new Vector3(-2f, -7f, 1f)); //2
        vertices.Add(new Vector3(1f, -7f, 1f)); //3
        vertices.Add(new Vector3(3f, -7f, 1f)); //4
        vertices.Add(new Vector3(1f, -6f, 1f)); //5
        vertices.Add(new Vector3(3f, -6f, 1f)); //6
        vertices.Add(new Vector3(-2f, -2f, 1f)); //7
        vertices.Add(new Vector3(-1f, -1f, 1f)); //8
        vertices.Add(new Vector3(1f, -1f, 1f)); //9
        vertices.Add(new Vector3(-1f, 0f, 1f)); //10
        vertices.Add(new Vector3(1f, 0f, 1f)); //11
        vertices.Add(new Vector3(3f, 0f, 1f)); //12
        vertices.Add(new Vector3(-1f, 6f, 1f)); //13
        vertices.Add(new Vector3(1f, 6f, 1f)); //14
        vertices.Add(new Vector3(3f, 6f, 1f)); //15
        vertices.Add(new Vector3(1f, 7f, 1f)); //16

        //back vertices
        vertices.Add(new Vector3(-3f, 7f, -1f)); //17
        vertices.Add(new Vector3(-3f, -7f, -1f)); //18
        vertices.Add(new Vector3(-2f, -7f, -1f)); //19
        vertices.Add(new Vector3(1f, -7f, -1f)); //20
        vertices.Add(new Vector3(3f, -7f, -1f)); //21
        vertices.Add(new Vector3(1f, -6f, -1f)); //22
        vertices.Add(new Vector3(3f, -6f, -1f)); //23
        vertices.Add(new Vector3(-2f, -2f, -1f)); //24
        vertices.Add(new Vector3(-1f, -1f, -1f)); //25
        vertices.Add(new Vector3(1f, -1f, -1f)); //26
        vertices.Add(new Vector3(-1f, 0f, -1f)); //27
        vertices.Add(new Vector3(1f, 0f, -1f)); //28
        vertices.Add(new Vector3(3f, 0f, -1f)); //29
        vertices.Add(new Vector3(-1f, 6f, -1f)); //30
        vertices.Add(new Vector3(1f, 6f, -1f)); //31
        vertices.Add(new Vector3(3f, 6f, -1f)); //32
        vertices.Add(new Vector3(1f, 7f, -1f)); //33
    }

    void defineFaces()
    {
        
            //texture faces with paint file coords
            faces = new List<Vector3Int>();
            texture_index_list = new List<Vector3Int>();
            normals = new List<Vector3>();
			//front(R is backwards)
            faces.Add(new Vector3Int(0, 1, 7));   texture_index_list.Add(new Vector3Int(0, 1, 4)); normals.Add(new Vector3(0, 0, -1)); //Face 0
            faces.Add(new Vector3Int(0, 7, 10)); texture_index_list.Add(new Vector3Int(0, 4, 6)); normals.Add(new Vector3(0, 0, -1)); //Face 1
			faces.Add(new Vector3Int(0, 10, 13)); texture_index_list.Add(new Vector3Int(0, 3, 2)); normals.Add(new Vector3(0, 0, -1)); //Face 2
			faces.Add(new Vector3Int(10, 7, 8)); texture_index_list.Add(new Vector3Int(3, 4, 6)); normals.Add(new Vector3(0, 0, -1)); //Face 3
			faces.Add(new Vector3Int(0, 13, 16)); texture_index_list.Add(new Vector3Int(0, 2, 11)); normals.Add(new Vector3(0, 0, -1)); //Face 4 
			faces.Add(new Vector3Int(16, 13, 14)); texture_index_list.Add(new Vector3Int(11, 2, 7)); normals.Add(new Vector3(0, 0, -1)); //Face 5
			faces.Add(new Vector3Int(16, 14, 15)); texture_index_list.Add(new Vector3Int(11, 7, 15)); normals.Add(new Vector3(0, 0, -1)); //Face 6
			faces.Add(new Vector3Int(14, 12, 15)); texture_index_list.Add(new Vector3Int(7, 16, 15)); normals.Add(new Vector3(0, 0, -1)); //Face 7
			faces.Add(new Vector3Int(14, 11, 12)); texture_index_list.Add(new Vector3Int(7, 8, 16)); normals.Add(new Vector3(0, 0, -1)); //Face 8
			faces.Add(new Vector3Int(11, 9, 12)); texture_index_list.Add(new Vector3Int(8, 12, 16)); normals.Add(new Vector3(0, 0, -1)); //Face 9 
			faces.Add(new Vector3Int(11, 8, 9)); texture_index_list.Add(new Vector3Int(8, 6, 12)); normals.Add(new Vector3(0, 0, -1)); //Face 10
			faces.Add(new Vector3Int(10, 8, 11)); texture_index_list.Add(new Vector3Int(3, 6, 8)); normals.Add(new Vector3(0, 0, -1)); //Face 11
			faces.Add(new Vector3Int(8, 7, 5)); texture_index_list.Add(new Vector3Int(6, 4, 9)); normals.Add(new Vector3(0, 0, -1)); //Face 12
			faces.Add(new Vector3Int(8, 5, 6)); texture_index_list.Add(new Vector3Int(6, 9, 13)); normals.Add(new Vector3(0, 0, -1)); //Face 13
			faces.Add(new Vector3Int(5, 3, 6)); texture_index_list.Add(new Vector3Int(9, 10, 13)); normals.Add(new Vector3(0, 0, -1)); //Face 14
			faces.Add(new Vector3Int(6, 3, 4)); texture_index_list.Add(new Vector3Int(13, 10, 14)); normals.Add(new Vector3(0, 0, -1)); //Face 15
			faces.Add(new Vector3Int(7, 1, 2)); texture_index_list.Add(new Vector3Int(4, 1, 5)); normals.Add(new Vector3(0, 0, -1)); //Face 16

			
			//back(R is backwards)
			faces.Add(new Vector3Int(17, 24, 18));  texture_index_list.Add(new Vector3Int(1, 2, 0)); normals.Add(new Vector3(0, 0, 1)); //Face 17
            faces.Add(new Vector3Int(17, 27, 24));  texture_index_list.Add(new Vector3Int(0, 3, 4)); normals.Add(new Vector3(0, 0, 1)); //Face 18
            faces.Add(new Vector3Int(17, 30, 27));  texture_index_list.Add(new Vector3Int(0, 2, 3)); normals.Add(new Vector3(0, 0, 1)); //Face 19
            faces.Add(new Vector3Int(27, 25, 24));  texture_index_list.Add(new Vector3Int(3, 6, 4)); normals.Add(new Vector3(0, 0, 1)); //Face 20
            faces.Add(new Vector3Int(17, 33, 30));  texture_index_list.Add(new Vector3Int(0, 11, 2)); normals.Add(new Vector3(0, 0, 1)); //Face 21
            faces.Add(new Vector3Int(33, 31, 30));  texture_index_list.Add(new Vector3Int(11, 7, 2)); normals.Add(new Vector3(0, 0, 1)); //Face 22
            faces.Add(new Vector3Int(33, 32, 31));  texture_index_list.Add(new Vector3Int(11, 15, 7)); normals.Add(new Vector3(0, 0, 1)); //Face 23
            faces.Add(new Vector3Int(32, 29, 31));  texture_index_list.Add(new Vector3Int(15, 16, 7)); normals.Add(new Vector3(0, 0, 1)); //Face 24
            faces.Add(new Vector3Int(31, 29, 28));  texture_index_list.Add(new Vector3Int(7, 16, 8)); normals.Add(new Vector3(0, 0, 1)); //Face 25
            faces.Add(new Vector3Int(28, 29, 26));  texture_index_list.Add(new Vector3Int(8, 16, 12)); normals.Add(new Vector3(0, 0, 1)); //Face 26
            faces.Add(new Vector3Int(28, 26, 25));  texture_index_list.Add(new Vector3Int(8, 12, 6)); normals.Add(new Vector3(0, 0, 1)); //Face 27
            faces.Add(new Vector3Int(27, 28, 25));  texture_index_list.Add(new Vector3Int(3, 8, 6)); normals.Add(new Vector3(0, 0, 1)); //Face 28
            faces.Add(new Vector3Int(25, 22, 24));  texture_index_list.Add(new Vector3Int(6, 9, 4)); normals.Add(new Vector3(0, 0, 1)); //Face 29
            faces.Add(new Vector3Int(25, 23, 22));  texture_index_list.Add(new Vector3Int(6, 13, 9)); normals.Add(new Vector3(0, 0, 1)); //Face 30
            faces.Add(new Vector3Int(22, 23, 21));  texture_index_list.Add(new Vector3Int(9, 13, 14)); normals.Add(new Vector3(0, 0, 1)); //Face 31
            faces.Add(new Vector3Int(22, 21, 20));  texture_index_list.Add(new Vector3Int(9, 14, 10)); normals.Add(new Vector3(0, 0, 1)); //Face 32
            faces.Add(new Vector3Int(24, 19, 18));  texture_index_list.Add(new Vector3Int(4, 5, 1)); normals.Add(new Vector3(0, 0, 1)); //Face 33
            
            //inner faces
            faces.Add(new Vector3Int(13, 30, 14));  texture_index_list.Add(new Vector3Int(21, 22, 23)); normals.Add(new Vector3(0, 0, 1)); //Face 34
            faces.Add(new Vector3Int(14, 30, 31));  texture_index_list.Add(new Vector3Int(23, 22, 24)); normals.Add(new Vector3(0, 0, 1)); //Face 35
            faces.Add(new Vector3Int(13, 10, 30));  texture_index_list.Add(new Vector3Int(17, 18, 19)); normals.Add(new Vector3(0, 0, 1)); //Face 36
            faces.Add(new Vector3Int(30, 10, 27));  texture_index_list.Add(new Vector3Int(19, 18, 20)); normals.Add(new Vector3(0, 0, 1)); //Face 37
            faces.Add(new Vector3Int(31, 28, 14));  texture_index_list.Add(new Vector3Int(17, 18, 19)); normals.Add(new Vector3(0, 0, 1)); //Face 38
            faces.Add(new Vector3Int(14, 28, 11));  texture_index_list.Add(new Vector3Int(19, 18, 20)); normals.Add(new Vector3(0, 0, 1)); //Face 39
            faces.Add(new Vector3Int(27, 10, 28));  texture_index_list.Add(new Vector3Int(21, 22, 23)); normals.Add(new Vector3(0, 0, 1)); //Face 40
            faces.Add(new Vector3Int(28, 10, 11));  texture_index_list.Add(new Vector3Int(23, 22, 24)); normals.Add(new Vector3(0, 0, 1)); //Face 41
            
            
            //outer faces
            faces.Add(new Vector3Int(17, 18, 0));  texture_index_list.Add(new Vector3Int(17, 18, 19)); normals.Add(new Vector3(0, 0, 1)); //Face 42
            faces.Add(new Vector3Int(0, 18, 1));  texture_index_list.Add(new Vector3Int(19, 18, 20)); normals.Add(new Vector3(0, 0, 1)); //Face 43
            faces.Add(new Vector3Int(17, 0, 33));  texture_index_list.Add(new Vector3Int(21, 22, 23)); normals.Add(new Vector3(0, 0, 1)); //Face 44
            faces.Add(new Vector3Int(33, 0, 16));  texture_index_list.Add(new Vector3Int(23, 22, 24)); normals.Add(new Vector3(0, 0, 1)); //Face 45
            faces.Add(new Vector3Int(33, 16, 32));  texture_index_list.Add(new Vector3Int(17, 18, 19)); normals.Add(new Vector3(0, 0, 1)); //Face 46
            faces.Add(new Vector3Int(32, 16, 15));  texture_index_list.Add(new Vector3Int(19, 18, 20)); normals.Add(new Vector3(0, 0, 1)); //Face 47
            faces.Add(new Vector3Int(15, 12, 32));  texture_index_list.Add(new Vector3Int(17, 18, 19)); normals.Add(new Vector3(0, 0, 1)); //Face 48
            faces.Add(new Vector3Int(32, 12, 29));  texture_index_list.Add(new Vector3Int(19, 18, 20)); normals.Add(new Vector3(0, 0, 1)); //Face 49
            faces.Add(new Vector3Int(12, 9, 29));  texture_index_list.Add(new Vector3Int(17, 18, 19)); normals.Add(new Vector3(0, 0, 1)); //Face 50
            faces.Add(new Vector3Int(29, 9, 26));  texture_index_list.Add(new Vector3Int(19, 18, 20)); normals.Add(new Vector3(0, 0, 1)); //Face 51
            faces.Add(new Vector3Int(8, 25, 9));  texture_index_list.Add(new Vector3Int(21, 22, 23)); normals.Add(new Vector3(0, 0, 1)); //Face 52
            faces.Add(new Vector3Int(9, 25, 26));  texture_index_list.Add(new Vector3Int(23, 22, 24)); normals.Add(new Vector3(0, 0, 1)); //Face 53
            faces.Add(new Vector3Int(25, 8, 23));  texture_index_list.Add(new Vector3Int(21, 22, 23)); normals.Add(new Vector3(0, 0, 1)); //Face 54
            faces.Add(new Vector3Int(23, 8, 6));  texture_index_list.Add(new Vector3Int(23, 22, 24)); normals.Add(new Vector3(0, 0, 1)); //Face 55
            faces.Add(new Vector3Int(6, 4, 23));  texture_index_list.Add(new Vector3Int(21, 22, 23)); normals.Add(new Vector3(0, 0, 1)); //Face 56
            faces.Add(new Vector3Int(23, 4, 21));  texture_index_list.Add(new Vector3Int(23, 22, 24)); normals.Add(new Vector3(0, 0, 1)); //Face 57
            faces.Add(new Vector3Int(4, 3, 21));  texture_index_list.Add(new Vector3Int(21, 22, 23)); normals.Add(new Vector3(0, 0, 1)); //Face 58
            faces.Add(new Vector3Int(21, 3, 20));  texture_index_list.Add(new Vector3Int(23, 22, 24)); normals.Add(new Vector3(0, 0, 1)); //Face 59
            faces.Add(new Vector3Int(22, 20, 5));  texture_index_list.Add(new Vector3Int(21, 22, 23)); normals.Add(new Vector3(0, 0, 1)); //Face 60
            faces.Add(new Vector3Int(5, 20, 3));  texture_index_list.Add(new Vector3Int(23, 22, 24)); normals.Add(new Vector3(0, 0, 1)); //Face 61
            faces.Add(new Vector3Int(7, 24, 5));  texture_index_list.Add(new Vector3Int(21, 22, 23)); normals.Add(new Vector3(0, 0, 1)); //Face 62
            faces.Add(new Vector3Int(5, 24, 22));  texture_index_list.Add(new Vector3Int(23, 22, 24)); normals.Add(new Vector3(0, 0, 1)); //Face 63
            faces.Add(new Vector3Int(7, 2, 24));  texture_index_list.Add(new Vector3Int(17, 18, 19)); normals.Add(new Vector3(0, 0, 1)); //Face 64
            faces.Add(new Vector3Int(24, 2, 19));  texture_index_list.Add(new Vector3Int(19, 18, 20)); normals.Add(new Vector3(0, 0, 1)); //Face 65
            faces.Add(new Vector3Int(2, 1, 19));  texture_index_list.Add(new Vector3Int(17, 18, 19)); normals.Add(new Vector3(0, 0, 1)); //Face 66
            faces.Add(new Vector3Int(19, 1, 18));  texture_index_list.Add(new Vector3Int(19, 18, 20)); normals.Add(new Vector3(0, 0, 1)); //Face 67
           
    }

    public GameObject CreateUnityGameObject()
	{
		Mesh mesh = new Mesh();
		GameObject newGO = new GameObject();
     
		MeshFilter mesh_filter = newGO.AddComponent<MeshFilter>();
		MeshRenderer mesh_renderer = newGO.AddComponent<MeshRenderer>();

		List<Vector3> coords = new List<Vector3>();
		List<int> dummy_indices = new List<int>();
		List<Vector2> text_coords = new List<Vector2>();
		List<Vector3> normalz = new List<Vector3>();

		for (int i = 0; i < faces.Count; i++)
		{
			Vector3 normal_for_face = normals[i];

			normal_for_face = new Vector3(normal_for_face.x, normal_for_face.y, -normal_for_face.z);

			coords.Add(vertices[faces[i].x]); dummy_indices.Add(i * 3); text_coords.Add(texture_coordinates[texture_index_list[i].x]); normalz.Add(normal_for_face);

			coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 1); text_coords.Add(texture_coordinates[texture_index_list[i].y]); normalz.Add(normal_for_face);

			coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 2); text_coords.Add(texture_coordinates[texture_index_list[i].z]); normalz.Add(normal_for_face);
		}

		mesh.vertices = coords.ToArray();
		mesh.triangles = dummy_indices.ToArray();
		mesh.uv = text_coords.ToArray();
		mesh.normals = normalz.ToArray();
		mesh_filter.mesh = mesh;

		return newGO;
	}
}
