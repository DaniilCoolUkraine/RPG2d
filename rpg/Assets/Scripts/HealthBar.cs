using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _pixelPerUnit;
    
    [SerializeField] private Unit _targetUnit;
    private float _unitMaxHeath;

    private void Start()
    {        
        _unitMaxHeath = _targetUnit.Health;
        UpdateScale(_targetUnit.Health);
    }
    
    public void UpdateScale(float scale)
    {
        gameObject.GetComponent<SpriteRenderer>().size = new Vector2(scale / (_unitMaxHeath / _pixelPerUnit), gameObject.GetComponent<SpriteRenderer>().size.y);
    }
}
