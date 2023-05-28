using UnityEngine;

public class NameGenerator : MonoBehaviour
{
    private const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public string GetRandomName(int nameLength)
    {
        char[] name = new char[nameLength];
        for (int i = 0; i < nameLength; i++)
        {
            name[i] = characters[Random.Range(0, characters.Length)];
        }

        return new string(name);

    }
}
