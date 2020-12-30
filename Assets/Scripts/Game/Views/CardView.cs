using DG.Tweening;
using Game.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public sealed class CardView : MonoBehaviour
    {
        [SerializeField] private Image _openImage;
        [SerializeField] private Image _closeImage;
        [SerializeField] private Button _clickButton;

        [Header("Animation Settings")]
        [SerializeField] private float _totalAnimationTime = 1f;
        [SerializeField] private Vector3 _scale = new Vector3(1.05f, 1.05f, 1f);

        private static readonly Vector3 RotateAngle = new Vector3(0f, 90f, 0f);
        
        private Sequence _animationSequence;
        private Contexts _contexts;

        public int Id { get; private set; }

        public void Init(int id)
        {
            Id = id;
        }

        public void Open()
        {
            PlaySequence(_closeImage, _openImage);
        }

        public void Close()
        {
            PlaySequence(_openImage, _closeImage);
        }

        private void Awake()
        {
            _contexts = Contexts.sharedInstance;
            
            _openImage.gameObject.TrySetActive(false);
            _closeImage.gameObject.TrySetActive(true);
            
            _clickButton
                .onClick
                .AddListener(OnCardClick);
        }

        private void OnDestroy()
        {
            _clickButton
                .onClick
                .RemoveListener(OnCardClick);
        }

        private void PlaySequence(Image startImage, Image endImage)
        {
            _animationSequence?.Kill();

            startImage.gameObject.TrySetActive(true);
            endImage.gameObject.TrySetActive(false);
            
            startImage.transform.localRotation = Quaternion.identity;
            startImage.transform.localScale = Vector3.one;
            endImage.transform.localRotation = Quaternion.Euler(RotateAngle);
            endImage.transform.localScale = _scale;

            var animationTime = _totalAnimationTime / 2f;

            _animationSequence = DOTween.Sequence()
                .Append(startImage.transform.DOLocalRotate(RotateAngle, animationTime)
                    .OnComplete(() =>
                    {
                        startImage.gameObject.TrySetActive(false);
                        endImage.gameObject.TrySetActive(true);
                    }))
                .Join(startImage.transform.DOScale(_scale, animationTime))
                .Append(endImage.transform.DOLocalRotate(Vector3.zero, animationTime)
                    .OnComplete(() =>
                    {
                        endImage.transform.localRotation = Quaternion.identity;
                    }))
                .Join(endImage.transform.DOScale(Vector3.one, animationTime))
                .Play();
        }

        private void OnCardClick()
        {
            var clickEntity = _contexts.game.CreateEntity();
            clickEntity.AddSelectCard(Id);
        }

        //TODO: debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
                Open();
            
            if (Input.GetKeyDown(KeyCode.P))
                Close();
        }
    }
}