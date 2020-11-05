namespace Learning.DAL.Generation.Proxies {
    public interface IProxy<TContext, TEntity> : IBaseProxy<TContext, TEntity> where TEntity : class {
    }
}
