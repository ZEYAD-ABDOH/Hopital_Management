namespace asw.Middlewares
{
    public class RateLimitingMiddlewares
    {
        private readonly RequestDelegate next;

        private static int counter = 0;

        private static DateTime _laskRequsertDate=DateTime.Now;


        public RateLimitingMiddlewares( RequestDelegate _next)
        {
            next = _next;
        }

        public async  Task Invoke(HttpContext content)
        {
            counter++;
            if (DateTime.Now.Subtract(_laskRequsertDate).Seconds > 10)
            {
                counter = 1;
                _laskRequsertDate = DateTime.Now;
                await next(content);
            }
            else {

                if (counter >5)
                {
                    _laskRequsertDate = DateTime.Now;
                     
                await  content.Response.WriteAsync("قد تم الوصول الى الاحد الاقصى الارجاء الانتظار ");
                }
                else
                {
                    _laskRequsertDate = DateTime.Now;
                    await next(content);

                }
            }

        }
    }
}
