using System;
using UnityEngine;

[Serializable]
public class UserInfo
{
    private int id;
    private string name;
    private string email;
    private string profileImageUrl;

    public int Id { get => id; }
    public string Name { get => name; }
    public string Email { get => email; }
    public string ProfileImageUrl { get => profileImageUrl; }
    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}
