using UnityEngine;
[CreateAssetMenu(fileName="New ProjectileStatsModifier", menuName = "States/new ProjectileStatsModifier")]
public class ProjectileStatsModifier : State
{
    [SerializeField] private float speedMultiplier;
    private float defSpeed;
    [SerializeField] private float damageMultiplier;
    private float defDamage;

    private ProjectileShooter projectileShooter;


    private Projectile projectile;
    public override void StartAffect(StatesManager newManager)
    {
        base.StartAffect(newManager);

        projectileShooter = manager.hostEntity.GetComponentInChildren<ProjectileShooter>();


    }

    public override void Affect()
    {
        if (currentTime > duration)
        {
            StopAffect();
        }
        else
        {
            if (projectileShooter != null && projectileShooter.Projectile != null)
            {
                projectile = projectileShooter.Projectile;
                if (projectile.damage == projectile.StartDamage || projectile.speedMultiplier == projectile.StartSpeed)
                {
                    projectileShooter.Projectile.SetNewValues(projectile.speedMultiplier * speedMultiplier, projectile.damage * damageMultiplier);
                }
            }
            //projectile = null;
            currentTime += Time.deltaTime;
        }
    }

    public override void StopAffect()
    {
        if (projectile != null)
        {
            projectileShooter.Projectile.SetNewValues(projectile.StartSpeed, projectile.StartDamage);

        }
        base.StopAffect();
    }
}