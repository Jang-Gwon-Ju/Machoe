using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class ScriptControl : MonoBehaviour {

    public Text name;
    public Text script;
    public string fileName;

    [Range(0, 1)]
    public float fps;

    int _scriptCount;
    bool _isOverlap;
    string _script;
    string _name;
    string _fullScript;
    char[] _divededScript = { };

    string _boldStart = "<b>";
    string _boldEnd = "</b>";
    string _italicStart = "<i>";
    string _italicEnd = "</i>";

    bool _isBold = false;
    bool _isItalic = false;

    TextAsset _textAsset;

	void OnEnable ()
    {
        checkFileExists();
        stringToChar();
        _fullScript = string.Empty;
        //_isBold = false;
        //_isItalic = false;

        StartCoroutine("printingScript");
    }

    void checkFileExists()
    {
        FileInfo check = new FileInfo(Application.dataPath + "/Resources/Script/" + fileName + ".txt");
        if (check.Exists)
        {
            _textAsset = Resources.Load("Script/" + fileName) as TextAsset;
            _script = string.Empty;
            _scriptCount = 0;
            _script = _textAsset.text;
            _isOverlap = false;
            Debug.Log("파일이 존재합니다.");
        }
        else
        {
            Debug.Log("Error! 파일이 존재하지 않습니다. : " + check.ToString() + " <- 경로(이름)가 맞나요?");
            this.enabled = false;
            return;
        }
    }

    public void pressButton()
    {
        if(!_isOverlap)
        {
            cleanUpDialog();
            stringToChar();
            StartCoroutine("printingScript");
        }
        else
        {
            cleanUpDialog();
            StopAllCoroutines();
            _isOverlap = false;
            script.text = _fullScript;
        }
    }

    void stringToChar()
    {
        char[] words = _script.ToCharArray();
        string[] temp = { string.Empty };
        string befor = string.Empty;

        _isBold = false;
        _isItalic = false;

        for (int i = _scriptCount; i < words.Length; i++)
        {
            if(words[i] != '\r')
            {
                befor += words[i].ToString();
            }
            else
            {
                _scriptCount = i + 2;
                temp = befor.Split('|');
                _name = temp[0];
                if (temp[1].EndsWith("<b>"))
                {
                    _isBold = true;
                    _divededScript = temp[1].Substring(0, temp[1].IndexOf("<b>")).ToCharArray();
                }
                else
                {
                    _divededScript = temp[1].ToCharArray();
                }
                
                _fullScript = new string(_divededScript);
                break;
            }
        }
    }

    IEnumerator printingScript()
    {
        _isOverlap = true;
        name.text = _name;

        for (int count = 0; count < _divededScript.Length; count++)
        {
            if (_isBold)
                script.text += "<b>" + _divededScript[count].ToString() + "</b>";
            else if (_isItalic)
                script.text += "<i>" + _divededScript[count].ToString() + "</i>";
            else
                script.text += _divededScript[count].ToString();

            yield return new WaitForSeconds(fps);
        }
        _isOverlap = false;
    }

    void cleanUpDialog()
    {
        script.text = "";
    }
}
