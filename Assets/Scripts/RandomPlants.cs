using UnityEngine;

public class RandomPlants : MonoBehaviour {
    public GameObject[] Plants;
    public int num;

	void Start () {
        for (int i = 0; i < num; i++)
        {
            int n = (int)(Random.value * Plants.Length);
            Vector3 pos = new Vector3(Random.Range(0, 1000), 0, Random.Range(0, 1000));
            Instantiate(Plants[n], pos, Quaternion.identity);
        }
	}
}
