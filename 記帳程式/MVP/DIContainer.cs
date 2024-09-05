using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳程式.Forms;

namespace 記帳程式.MVP
{
    internal class DIContainer
    {
        static ServiceCollection collection = new ServiceCollection();
        static ServiceProvider provider = null;
        public static void Register()
        {
            collection.AddScoped<IAddFormPresenter, AddFormPresenter>();
            collection.AddSingleton<IAddFormView, AddForm>();
            collection.AddScoped<INoteFormPresenter, NoteFormPresenter>();
            collection.AddSingleton<INoteFormView, NoteForm>();
            collection.AddScoped<ISearchFormPresenter, SearchFormPresenter>();
            collection.AddSingleton<ISearchFormView, AccountForm>();
            collection.AddScoped<IRepository, Repository>();
            collection.AddSingleton<IGraphicFormView, GraphicForm>();
            collection.AddScoped<IGraphicFormPresenter,GraphicFormPresenter>();
            provider = collection.BuildServiceProvider();
        }

        public static T GetInstance<T>()
        {
            return provider.GetService<T>();
        }
    }
}
