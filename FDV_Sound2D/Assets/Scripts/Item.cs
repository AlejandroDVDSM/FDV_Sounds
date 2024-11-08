using DefaultNamespace;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip _rewardClip;
    [SerializeField] private AudioClip _healingClip;
    
    [SerializeField] private EItemType _itemType;
    
    public EItemType ItemType => _itemType;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _sfxSource.clip = _itemType == EItemType.Healing ? _healingClip : _rewardClip;    
            _sfxSource.Play();
            
            Destroy(gameObject);
        }
    }
}
