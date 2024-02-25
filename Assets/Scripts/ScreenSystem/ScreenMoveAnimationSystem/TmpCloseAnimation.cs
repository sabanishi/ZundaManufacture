using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sabanishi.ScreenSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Sabanishi.ZundaManufacture.ScreenSystem
{
    public class TmpCloseAnimation:ITransitionAnimation
    {
        private readonly Image _image;
        public TmpCloseAnimation(Image image)
        {
            _image = image;
        }
        
        public async UniTask Play(CancellationToken token)
        {
            _image.enabled = true;
            _image.gameObject.SetActive(true);
            _image.color = new Color(0, 0, 0, 0);
            //0.5秒かけて暗転させる
            await _image.DOFade(1, 0.5f).ToUniTask(cancellationToken: token);
        }
    }
}