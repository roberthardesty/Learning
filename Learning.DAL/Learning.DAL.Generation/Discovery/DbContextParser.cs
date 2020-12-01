using Learning.DAL.Generation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Learning.DAL.Generation.Discovery {

    public class DbContextParser {

        private Type ContextType { get; }

        public DbContextParser(Type contextType) {
            Assert.True<InvalidOperationException>(contextType.GetTypeInfo().IsSubclassOf(typeof(DbContext)), () => "Non context supplied");
            ContextType = contextType;
        }

        public ApiModel Construct() {

            return new ApiModel(CreateControllerModels().AsEnumerable());
        }
        private IEnumerable<ControllerModel> CreateControllerModels()
        {
            EntityParser parser = new EntityParser();

            foreach (var resourceCollection in FindResourceCollections())
            {
                ControllerModel cModel;
                try
                {
                    cModel = parser.Dissect(resourceCollection);
                }
                catch (Exception)
                {
                    continue;
                }

                yield return cModel;
            }
        }
        private IEnumerable<SetProperties> FindResourceCollections() {
            return ContextType
                    .GetProperties()
                    .Where(inf => !inf.GetCustomAttributes<ApiExclusionAttribute>().Any() && inf.PropertyType.IsConstructedGenericType)
                    .Select(inf => new SetProperties {
                                        EntityType = inf.PropertyType.GetGenericArguments().First(),
                                        Attributes = inf.GetCustomAttributes(),
                                        Name = inf.Name
                    });
        }

    }
}
