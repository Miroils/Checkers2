using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;


public class Checker2D : MonoBehaviour
{
    const string QUEEN_CHECKER_STRING = "chkr_queen";
    const string NORMAL_CHECLER_STRING = "chkr_norm";
    [SerializeField] private SpriteRenderer _CheckerSprite;
    //27 08 [SerializeField] private AssetReference _CheckersAssets;
    private Color _greenChecker = new Color(0.0f, 1.0f, 0.0f);//19 08 тестово дефолтные цвета
    private Color _redChecker = new Color(1.0f, 0.0f, 0.0f);//19 08 тестово дефолтные цвета

    private Checker _checker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //19 08 попытка через конструктор создавать
    public void Initialization(Checker checker)
    {
        _checker = checker;
        _checker.CheckerDestroyedAction += Checker2DDestroy;
        _checker.CheckerReturnedAction += Checker2DReturn;
        _checker.CheckerPromoutedAction += Update2DSprite;
        _checker.CheckerDemoutedAction += Update2DSprite;
        UpdateCheckerVisual();
    }

    void UpdateCheckerVisual()
    {
        Update2DSprite();
        if (_checker.GetCheckerColor() == CheckerColorEnum.redChecker)
        {
            ColoringToRed();
        }
        else
        {
            ColoringToGreen();
        }
    }

    private void Update2DSprite()
    {
        if (_checker.IsQueen())
        {
            StartCoroutine(LoadCheckerSpirte(QUEEN_CHECKER_STRING));
        }
        else
        {
            StartCoroutine(LoadCheckerSpirte(NORMAL_CHECLER_STRING));
        }
    }

    private IEnumerator LoadCheckerSpirte(string spriteName)
    {
        //AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetsAsync(spriteName, ); //_CheckersAssets.LoadAssetAsync<Sprite>();
        var handle = Addressables.LoadAssetAsync<Sprite>(spriteName); //_CheckersAssets.LoadAssetAsync<Sprite>();
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite sprite = handle.Result;
            _CheckerSprite.sprite = sprite;

            
            Addressables.Release(handle);
        }
    }

    private void ColoringToRed()
    {
        _CheckerSprite.color = _redChecker;
    }
    private void ColoringToGreen()
    {
        _CheckerSprite.color = _greenChecker;
    }

    private void Checker2DDestroy()
    {
        gameObject.SetActive(false);
    }

    private void Checker2DReturn()
    {
        gameObject.SetActive(true);
    }
}
