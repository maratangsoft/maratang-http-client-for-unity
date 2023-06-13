using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public Text numText;
    public Text titleText;
    public Text singerText;

    public void Bind(Song song)
    {
        numText.text = song.No;
        titleText.text = song.Title;
        singerText.text = song.Singer;
	}
}
