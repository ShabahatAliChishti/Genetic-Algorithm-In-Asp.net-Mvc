namespace TimeTable.App.Views
{
    public class TimeSlotDTO
    {
        public int Day { get; set; }
        public double Start { get; set; }
        public double End { get; set; }
        public string Place { get; set; }
        public string Course { get; set; }
        public int Students { get; set; }
    }

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Device.InvokeOnMainThreadAsync(Init);

        }

        public async Task Init()
        {
            var currentWeek = DateTime.Today.AddDays(DateTime.Today.Day % 7);
            using (var httpClient = new HttpClient())
            {

                var serverResponse = await httpClient.GetStringAsync("http://d73f96fd2125.ngrok.io/api/timetable");
                var data = JsonConvert.DeserializeObject<List<TimeSlotDTO>>(serverResponse);
                schedule.DataSource = data.Select(item => new ScheduleAppointment()
                {
                    Subject = item.Course,
                    StartTime = currentWeek.AddDays(item.Day).AddMilliseconds(item.Start),
                    EndTime = currentWeek.AddDays(item.Day).AddMilliseconds(item.End),
                    Location = item.Place

                }).ToList();

            }
        }

    }
}