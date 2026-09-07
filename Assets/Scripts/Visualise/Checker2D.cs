using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Checker2D : MonoBehaviour
{
    const string QUEEN_CHECKER_STRING = "chkr_queen";
    const string NORMAL_CHECLER_STRING = "chkr_norm";
    [SerializeField] private SpriteRenderer _checkerSprite;
    private Color _greenChecker = new Color(0.0f, 1.0f, 0.0f);//19 08 тестово дефолтные цвета
    private Color _redChecker = new Color(1.0f, 0.0f, 0.0f);//19 08 тестово дефолтные цвета
    private Checker _checker;

    public void Initialization(Checker checker)
    {
        _checker = checker;
        _checker.CheckerDestroyedAction += Checker2DDestroy;
        _checker.CheckerReturnedAction += Checker2DReturn;
        _checker.CheckerPromoutedAction += Update2DSprite;
        _checker.CheckerDemoutedAction += Update2DSprite;
        UpdateCheckerVisual();
    }

    private void UpdateCheckerVisual()
    {
        Update2DSprite();
        if (_checker.GetCheckerColor() == CheckerColorEnum.RedChecker)
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
        var handle = Addressables.LoadAssetAsync<Sprite>(spriteName);
        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite sprite = handle.Result;
            _checkerSprite.sprite = sprite;            
            Addressables.Release(handle);
        }
    }

    private void ColoringToRed()
    {
        _checkerSprite.color = _redChecker;
    }

    private void ColoringToGreen()
    {
        _checkerSprite.color = _greenChecker;
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
