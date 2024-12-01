using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private int maxHealth;  //总生命值
    public int currentHealth;

    [SerializeField] private int maxCoinValue;
    public int MaxCoinValue{get{return maxCoinValue;}}
    public int currentCoin;
    
    private void Start() {
        currentHealth = maxHealth;
    }

    private void OnEnable() {
        EventHandler.GainCoin += OnGainCoin;
    }

    private void OnDisable() {
        EventHandler.GainCoin -= OnGainCoin;
    }

    private void OnGainCoin()
    {
        Gain();
    }

    public void Damage(int damage){
        currentHealth -= damage;


        if(currentHealth <= 0){
            UIManager.Singleton.UpdatePlayerHealth(0);
            //玩家死亡
            GameManager.Singleton.Fail();
            return;
        }

        UIManager.Singleton.UpdatePlayerHealth((float)currentHealth/maxHealth);
        
    }

    public void Heal(){
        currentHealth = maxHealth;
        UIManager.Singleton.UpdatePlayerHealth((float)currentHealth/maxHealth);
    }

    public void Gain(){
        currentCoin++;
        if(currentCoin > maxCoinValue){
            currentCoin = maxCoinValue;
        }
        UIManager.Singleton.UpdateCoinBar((float)currentCoin/maxCoinValue);
        // Debug.Log((float)currentCoin/maxCoinValue);
    }

    public void Punish(){
        currentHealth = currentHealth/2;
        if(currentHealth == 0){currentHealth = 1;}
        UIManager.Singleton.UpdatePlayerHealth((float)currentHealth/maxHealth);
    }
}
