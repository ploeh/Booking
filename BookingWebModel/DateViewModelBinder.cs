using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Ploeh.Samples.Booking.WebModel
{
    public class DateViewModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var id = bindingContext.ValueProvider.GetValue("id");
            var date = DateTime.ParseExact(id.AttemptedValue, "yyyy.MM.dd", id.Culture);
            return new DateViewModel(date);
        }
    }
}
