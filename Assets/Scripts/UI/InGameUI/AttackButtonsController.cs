using UnityEngine;
using UnityEngine.UI;

public class AttackButtonsController : MonoBehaviour
{
    public Image m_ItemIcon;
    [SerializeField] private CooldownVisualUI m_CooldownController;
    public float m_AttackCooldown = 0;
    public PlayerCombatScript m_PlayerCombatScript;

    public ButtonType m_ButtonType = ButtonType.NONE;

    public enum ButtonType { NONE = -1, ABILITY, ACTIVE, LIGHT_ATTACK, HEAVY_ATTACK }

    private void OnEnable()
    {
        m_PlayerCombatScript = FindAnyObjectByType<PlayerCombatScript>();
        if(m_AttackCooldown == 0 )
        {
            UpdateItem();
        }
    }

    private void Start()
    {
        //UpdateItem();
    }

    public void UpdateItem()
    {
        float cooldown = 0;
        switch (m_ButtonType)
        {
            case ButtonType.ABILITY:
                m_ItemIcon.sprite = AbilityManager.instance.m_CurrentAbilitySprite;
                cooldown = AbilityManager.instance.m_AbilityTotalCooldown;
                break;
            case ButtonType.ACTIVE:
                if (InventoryManager.instance.m_CurrentActiveItem != null)
                {
                    m_ItemIcon.sprite = InventoryManager.instance.m_CurrentActiveItem.m_SpriteItem;
                    cooldown = InventoryManager.instance.m_CurrentActiveItem.m_ItemCooldown;
                    break;
                }
                gameObject.SetActive(false);
                break;
            case ButtonType.LIGHT_ATTACK:
                m_ItemIcon.sprite = InventoryManager.instance.m_CurrentLightWeapon.m_SpriteItem;
                cooldown = InventoryManager.instance.m_CurrentLightWeapon.m_ItemCooldown;
                break;
            case ButtonType.HEAVY_ATTACK:
                m_ItemIcon.sprite = InventoryManager.instance.m_CurrentHeavyWeapon.m_SpriteItem;
                cooldown = InventoryManager.instance.m_CurrentHeavyWeapon.m_ItemCooldown;
                break;
        }
        m_AttackCooldown = cooldown;
    }

    public void ActivateButton()
    {
        if (m_CooldownController.TryButton(m_AttackCooldown))
        {
            switch (m_ButtonType)
            {
                case ButtonType.ABILITY:
                    AbilityManager.instance.UseAbility();
                    break;
                case ButtonType.ACTIVE:
                    m_PlayerCombatScript.UseActiveItem();
                    break;
                case ButtonType.LIGHT_ATTACK:
                    m_PlayerCombatScript.LightAttack();
                    break;
                case ButtonType.HEAVY_ATTACK:
                    m_PlayerCombatScript.HeavyAttack();
                    break;
            }
        }
    }
}
