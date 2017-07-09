using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace LanguageFeatures1.Models
{
    public class MyAsyncMethods
    {
        public static Task<long?> GetPageLength()
        {
            HttpClient client = new HttpClient();
            var httpTask = client.GetAsync("http://apress.com");
            // мы можем здесь делать другие вещи, пока мы ждем
            // окончания HTTP запроса
            return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent/*предшествующее*/) =>
            {
                return antecedent.Result.Content.Headers.ContentLength;
            });
        }
        // The same but a little bit simplest!!!!!!!!!
        //public async static Task<long?> GetPageLength()
        //{
        //    HttpClient client = new HttpClient();
        //    var httpMessage = await client.GetAsync("http://apress.com");
        //    // мы можем здесь делать другие вещи, пока мы ждем
        //    // окончания HTTP запроса
        //    return httpMessage.Content.Headers.ContentLength;
        //}
    }
}
/*Это простой метод, который использует объект System.Net.Http.HttpClient для запроса
содержимого страницы Apress и возвращает ее длину
.NET будет работать асинхронно используя Task. Объекты Task строго типизированы и
основываются на результате, который возвращает работа в фоновом режиме. Так что когда мы
вызываем метод HttpClient.GetAsync, то, что нам вернется, будет Task<HttpResponseMessage>.
Это говорит нам о том, что запрос будет выполняться в фоновом режиме и о том, что результат
запроса будет объектом HttpResponseMessage.
возобновление задачи. Это
механизм, при помощи которого вы указываете, что должно произойти, когда будет выполнена
фоновая задача. В этом примере мы использовали метод ContinueWith для обработки объекта
HttpResponseMessage который мы получаем от метода HttpClient.GetAsync. Это мы сделали при
помощи лямбда-выражения, которое возвращает значение свойства, возвращающего длину
контента, которой мы получили от веб-сервера Apress. Обратите внимание, что мы дважды
используем ключевое слово return:
return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent) => {
return antecedent.Result.Content.Headers.ContentLength;
});
Это та часть, которая доставляет головную боль. Первое использование ключевого слова return
указывает на то, что мы возвращаем объект Task<HttpResponseMessage>, который после
выполнения задачи вернет (будет использовать return) длину заголовка ContentLength.
Заголовок ContentLength возвращает результат long?, и это обозначает, что результат нашего
метода GetPageLength будет Task<long?>:
public static Task<long?> GetPageLength() {
****************page94***********
 */
