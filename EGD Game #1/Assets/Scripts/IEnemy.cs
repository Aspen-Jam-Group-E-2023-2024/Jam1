using System.Collections;

public interface IEnemy
{ 
    public IEnumerator Hurt(int damage, float knockbackForce);
}
