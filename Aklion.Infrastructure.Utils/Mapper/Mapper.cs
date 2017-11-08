//namespace Aklion.Infrastructure.Utils.Mapper
//{
//    public static class Mapper
//    {
//        public static TNewModel MapNew<TOldModel, TNewModel>(this TOldModel oldModel) where TNewModel : new()
//        {
//            var newModel = new TNewModel();
//            var newModelType = newModel.GetType();

//            foreach (var oldModelProperty in oldModel.GetType().GetProperties())
//            {
//                var newModelProperty = newModelType.GetProperty(oldModelProperty.Name);

//                if (oldModelProperty.PropertyType != newModelProperty.PropertyType)
//                {
//                    continue;
//                }

//                var oldModelPropertyValue = oldModelProperty.GetValue(oldModel);

//                newModelProperty.SetValue(newModel, oldModelPropertyValue);
//            }

//            return newModel;
//        }

//        public static void Map<TSrcModel, TDstModel>(this TSrcModel srcModel, TDstModel dstModel)
//        {
//            var dstModelType = dstModel.GetType();

//            foreach (var srcModelProperty in srcModel.GetType().GetProperties())
//            {
//                var dstModelProperty = dstModelType.GetProperty(srcModelProperty.Name);

//                if (srcModelProperty.PropertyType != dstModelProperty)
//                {
//                    continue;
//                }

//                var srcModelPropertyValue = srcModelProperty.GetValue(srcModel);

//                dstModelProperty.SetValue(dstModel, srcModelPropertyValue);
//            }
//        }
//    }
//}