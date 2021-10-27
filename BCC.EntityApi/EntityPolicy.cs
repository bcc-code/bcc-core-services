using System.Threading.Tasks;

namespace BCC.EntityApi
{
    public class EntityPolicy<TEntity> where TEntity : BaseEntity
    {
        protected bool ALLOW_BY_DEFAULT = true;

        public virtual Task<bool> CanViewAny()
        {
            return Task.FromResult(ALLOW_BY_DEFAULT);
        }

        public virtual Task<bool> CanView(TEntity entity)
        {
            return Task.FromResult(ALLOW_BY_DEFAULT);
        }

        public virtual Task<bool> CanCreate(TEntity entity = null)
        {
            return Task.FromResult(ALLOW_BY_DEFAULT);
        }

        public virtual Task<bool> CanUpdate(TEntity entity)
        {
            return Task.FromResult(ALLOW_BY_DEFAULT);
        }

        public virtual Task<bool> CanDelete(TEntity entity = null)
        {
            return Task.FromResult(ALLOW_BY_DEFAULT);
        }
    }
}