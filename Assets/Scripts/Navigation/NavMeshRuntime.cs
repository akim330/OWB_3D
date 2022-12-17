
using UnityEngine;
using UnityEngine.AI;

public class NavMeshRuntime : MonoBehaviour
{
    private NavMeshSurface _surface;

    // Start is called before the first frame update
    void Awake()
    {
        _surface = GetComponent<NavMeshSurface>();
    }

    public void Bake()
    {
        if (_surface == null)
        {
            _surface = GetComponent<NavMeshSurface>();
        }
        Debug.Log("Baking nav mesh");
        _surface.BuildNavMesh();
    }
}
