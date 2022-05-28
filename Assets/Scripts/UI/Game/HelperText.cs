using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperText : MonoBehaviour
{

    private string unselect_text = "Pressione ESC para desselecionar";

    private int unselect_fontSize = 8;

    private string select_text = "Selecione um jogador";

    private int select_fontSize = 12;

    private string string_show;

    public Text helperText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteUnselectPlayerText(){
        helperText.text = unselect_text;
        helperText.fontSize = unselect_fontSize;
    }

    public void WriteSelectPlayerText(){
        helperText.text = select_text;
        helperText.fontSize = select_fontSize;
    }
}
