using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoretext;
    [SerializeField]
    private Image _livesimg;
    [SerializeField]
    private Sprite[] _lives;
    [SerializeField]
    private Text _gameover;
    [SerializeField]
    private Text _restart;
    private game_manager _gm;
    // Start is called before the first frame update
    void Start()
    {
        _scoretext.text = "Score:" + 0;
        _gameover.gameObject.SetActive(false);
        _gm = FindObjectOfType<game_manager>();
        if(_gm==null)
        {
            Debug.LogError("it is null");
        }
    }
    public void updatescore(int playerscore)
    {
        _scoretext.text = "Score:" + playerscore;
    }
    public void updatelives(int currentlives)
    {
        _livesimg.sprite = _lives[currentlives];
    }
    public void updategameover()
    {
        _gm.gameover();
        _gameover.gameObject.SetActive(true);
        _restart.gameObject.SetActive(true);
        StartCoroutine(gameflicker());
    }
    IEnumerator gameflicker()
    {
        while(true)
        {
            _gameover.text = "game over";
            yield return new WaitForSeconds(0.5f);
            _gameover.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
