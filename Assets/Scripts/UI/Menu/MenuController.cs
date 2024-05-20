using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _selectLevelPanel;
    [SerializeField] private Transform _posSettings;
    [SerializeField] private Transform _posMineMenu;
    [SerializeField] private Transform _selectLevelPos;
    [SerializeField] private float _speedCam = 1000f;

    private bool _isNeedToMoveCamera = false;
    private Camera _camera;
    private Transform _targetPos;

    private void Start()
    {
        _camera = Camera.main;
    }
    private void FixedUpdate()
    {
        CameraControl();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(LevelSectionHandler.unlockedLevels);
        //GameManager.OnGameStarted.Invoke();
    }
    public void Settings()
    {
        _isNeedToMoveCamera = true;   
        _targetPos = _posSettings;
    }
    public void ReturnToMainMenu()
    {
        _isNeedToMoveCamera = true;
        _targetPos = _posMineMenu;
    }
    public void LevelSelect()
    {
        _isNeedToMoveCamera = true;
        _targetPos = _selectLevelPos;
    }

    public void DiscordLink() => Application.OpenURL("https://discord.gg/uu3hnegEpD");
    public void TwitchLink() => Application.OpenURL("https://www.twitch.tv/taimastavern");
    public void YoutubeLink() => Application.OpenURL("https://www.youtube.com/channel/UC96PX4-tUTPHYF9XTFbUp2Q");
    public void TelegramLink() => Application.OpenURL("https://t.me/infokasai");
    public void BoostyLink() => Application.OpenURL("https://boosty.to/taima");
    private void CameraControl()
    {
        if( _isNeedToMoveCamera )
        {
            if( _camera.gameObject.transform.position != _targetPos.position)
            {
                CameraMove();
            }
            else
            {
                _isNeedToMoveCamera = false;
            }
        }
    }
    private void CameraMove()
    {
        _camera.gameObject.transform.position = Vector3.MoveTowards(_camera.gameObject.transform.position, _targetPos.position, _speedCam * Time.deltaTime);
    }
}
