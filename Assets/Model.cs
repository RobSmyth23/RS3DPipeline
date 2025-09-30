using UnityEngine;
using System.Collections.Generic;
using System;

public class Model
{
	List<Vector3> vertices;
    List<Vector2> texture_coordinates;
    List<Vector3Int> texture_index_list;
    List<Vector3> normals;

    List<Vector3Int> faces;

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
        uv_list.Add(new Vector2Int(105,399));
        uv_list.Add(new Vector2Int(105, 883));
        uv_list.Add(new Vector2Int(206,467));

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
        
            //front faces
            faces = new List<Vector3Int>();
            texture_index_list = new List<Vector3Int>();
            normals = new List<Vector3>();

            faces.Add(new Vector3Int(0, 1, 7));   texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1)); //Face 0
            faces.Add(new Vector3Int(0, 7, 10)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1)); //Face 1
        faces.Add(new Vector3Int(0, 10, 13)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(10, 7, 8)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(0, 13, 16)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(16, 13, 14)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(16, 14, 15)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(14, 12, 15)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(14, 11, 12)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(11, 9, 12)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(11, 8, 9)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(10, 8, 11)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(8, 7, 5)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(8, 5, 6)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(5, 3, 6)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(6, 3, 4)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));
        faces.Add(new Vector3Int(7, 1, 2)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, 1));

        //tried adding back faces, makes the object look weird, ask rob before proceeding further! Also re-explain what these normals mean and how to dictate where
        //in the paint image the coords go!
        faces.Add(new Vector3Int(17, 24, 18)); texture_index_list.Add(new Vector3Int(0, 1, 2)); normals.Add(new Vector3(0, 0, -1));

        /*
            faces.Add(new Vector3Int(13, 1, 10)); //Face 1
            faces.Add(new Vector3Int(7, 1, 2)); //Face 2
            faces.Add(new Vector3Int(10, 7, 8)); //Face 3
            faces.Add(new Vector3Int(0, 13, 14)); //Face 4
            faces.Add(new Vector3Int(0, 14, 16)); //Face 5
            faces.Add(new Vector3Int(16, 14, 15)); //Face 6
            faces.Add(new Vector3Int(14, 11, 15)); //Face 7
            faces.Add(new Vector3Int(15, 11, 12)); //Face 8
            faces.Add(new Vector3Int(11, 9, 12)); //Face 9 
            faces.Add(new Vector3Int(11, 8, 9)); //Face 10
            faces.Add(new Vector3Int(11, 10, 8)); //Face 11
            faces.Add(new Vector3Int(7, 5, 8)); //Face 12
            faces.Add(new Vector3Int(8, 5, 6)); //Face 13
            faces.Add(new Vector3Int(6, 5, 3)); //Face 14
            faces.Add(new Vector3Int(6, 3, 4)); //Face 15

            //back faces
            faces.Add(new Vector3Int(30, 18, 17)); //Face 16
            faces.Add(new Vector3Int(27, 18, 30)); //Face 17
            faces.Add(new Vector3Int(19, 18, 24)); //Face 18
            faces.Add(new Vector3Int(25, 24, 27)); //Face 19
            faces.Add(new Vector3Int(31, 30, 17)); //Face 20
            faces.Add(new Vector3Int(33, 31, 17)); //Face 21
            faces.Add(new Vector3Int(32, 31, 33)); //Face 22
            faces.Add(new Vector3Int(29, 28, 31)); //Face 23
            faces.Add(new Vector3Int(29, 31, 32)); //Face 24
            faces.Add(new Vector3Int(29, 26, 28)); //Face 25 
            faces.Add(new Vector3Int(26, 27, 28)); //Face 26
            faces.Add(new Vector3Int(26, 25, 27)); //Face 27
            faces.Add(new Vector3Int(25, 22, 24)); //Face 28
            faces.Add(new Vector3Int(23, 22, 25)); //Face 29
            faces.Add(new Vector3Int(21, 22, 23)); //Face 30
            faces.Add(new Vector3Int(21, 20, 22)); //Face 31

            //side faces 
            faces.Add(new Vector3Int(0, 17, 1));
            faces.Add(new Vector3Int(1, 17, 18));
            faces.Add(new Vector3Int(1, 18, 2));
            faces.Add(new Vector3Int(2, 18, 19));
            faces.Add(new Vector3Int(2, 19, 7));
            faces.Add(new Vector3Int(7, 19, 24));
            faces.Add(new Vector3Int(7, 24, 10));
            faces.Add(new Vector3Int(10, 24, 27));
            faces.Add(new Vector3Int(10, 27, 13));
            faces.Add(new Vector3Int(13, 27, 30));
            faces.Add(new Vector3Int(13, 30, 14));
            faces.Add(new Vector3Int(14, 30, 31));
            faces.Add(new Vector3Int(14, 31, 15));
            faces.Add(new Vector3Int(15, 31, 32));
            faces.Add(new Vector3Int(15, 32, 12));
            faces.Add(new Vector3Int(12, 32, 29));
            faces.Add(new Vector3Int(12, 29, 11));
            faces.Add(new Vector3Int(11, 29, 28));
            faces.Add(new Vector3Int(11, 28, 8));
            faces.Add(new Vector3Int(8, 28, 25));
            faces.Add(new Vector3Int(8, 25, 5));
            faces.Add(new Vector3Int(5, 25, 22));
            faces.Add(new Vector3Int(5, 22, 3));
            faces.Add(new Vector3Int(3, 22, 20));
            faces.Add(new Vector3Int(3, 20, 4));
            faces.Add(new Vector3Int(4, 20, 21));
            faces.Add(new Vector3Int(4, 21, 6));
            faces.Add(new Vector3Int(6, 21, 23));
        */

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

			coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 2); text_coords.Add(texture_coordinates[texture_index_list[i].y]); normalz.Add(normal_for_face);

			coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 1); text_coords.Add(texture_coordinates[texture_index_list[i].z]); normalz.Add(normal_for_face);
		}

		mesh.vertices = coords.ToArray();
		mesh.triangles = dummy_indices.ToArray();
		mesh.uv = text_coords.ToArray();
		mesh.normals = normalz.ToArray();
		mesh_filter.mesh = mesh;

		return newGO;
	}
}
