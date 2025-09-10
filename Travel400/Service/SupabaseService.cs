using Supabase;
using Travel400.Model;

namespace Travel400.Service
{
    public class SupabaseService
    {
        private readonly Client _supabaseClient;

        public SupabaseService(Client client)
        {
            _supabaseClient = client;
        }

        // ---------------------------
        // 📌 الحجوزات (Booking)
        // ---------------------------

        public async Task<bool> AddBookingAsync(Booking booking)
        {
            var result = await _supabaseClient
                .From<Booking>()
                .Insert(booking);

            return result.Models.Count > 0;
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            var result = await _supabaseClient
                .From<Booking>()
                .Get();

            return result.Models;
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            var result = await _supabaseClient
                .From<Booking>()
                .Where(b => b.Id == id)
                .Get();

            return result.Models.FirstOrDefault();
        }

        public async Task DeleteBookingAsync(int id)
        {
            await _supabaseClient
                .From<Booking>()
                .Where(b => b.Id == id)
                .Delete();
        }

        public async Task CancelBookingAsync(int id)
        {
            var bookings = await _supabaseClient
                .From<Booking>()
                .Where(b => b.Id == id)
                .Get();

            var booking = bookings.Models.FirstOrDefault();
            if (booking != null)
            {
                booking.IsCanceled = true;
                await _supabaseClient.From<Booking>().Update(booking);
            }
        }

        public async Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            var result = await _supabaseClient
                .From<Booking>()
                .Where(b => b.CustomerId == customerId)
                .Get();

            return result.Models;
        }

        public async Task<List<Booking>> GetBookingsByCountryAsync(string country)
        {
            var result = await _supabaseClient
                .From<Booking>()
                .Where(b => b.CountryName == country)
                .Get();

            return result.Models;
        }

        public async Task<int> GetTotalBookingsCountAsync()
        {
            var result = await _supabaseClient.From<Booking>().Get();
            return result.Models.Count;
        }

        public async Task<int> GetCanceledBookingsCountAsync()
        {
            var result = await _supabaseClient
                .From<Booking>()
                .Where(b => b.IsCanceled == true)
                .Get();

            return result.Models.Count;
        }

        public async Task<string?> GetMostPopularCountryAsync()
        {
            var result = await _supabaseClient.From<Booking>().Get();

            var grouped = result.Models
                .Where(b => !b.IsCanceled)
                .GroupBy(b => b.CountryName)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            return grouped?.Key;
        }

        public async Task<Dictionary<string, (int totalSeats, int remainingSeats)>> GetSeatsStatusPerCountryAsync()
        {
            var result = await _supabaseClient.From<Booking>().Get();

            var grouped = result.Models
                .Where(b => !b.IsCanceled)
                .GroupBy(b => b.CountryName)
                .ToDictionary(
                    g => g.Key,
                    g =>
                    {
                        var total = g.Sum(b => b.SeatsRequested);
                        var used = g.Count(); // مثال: كل حجز يمثل حجز مقعد
                        return (total, total - used);
                    });

            return grouped;
        }

        // ---------------------------
        // 👤 الزبائن (Customer)
        // --------------------
        //--------

        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            var result = await _supabaseClient
                .From<Customer>()
                .Insert(customer);

            return result.Models.Count > 0;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var result = await _supabaseClient
                .From<Customer>()
                .Get();

            return result.Models;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            var result = await _supabaseClient
                .From<Customer>()
                .Where(c => c.Id == id)
                .Get();

            return result.Models.FirstOrDefault();
        }

        public async Task<Customer?> GetCustomerByPassportAsync(string passport)
        {
            var result = await _supabaseClient
                .From<Customer>()
                .Where(c => c.PassportNumber == passport)
                .Get();

            return result.Models.FirstOrDefault();
        }

        public async Task<Customer?> GetCustomerByPhoneAsync(string phone)
        {
            var result = await _supabaseClient
                .From<Customer>()
                .Where(c => c.Phone == phone)
                .Get();

            return result.Models.FirstOrDefault();
        }

        public async Task<List<Customer>> SearchCustomersByNameAsync(string name)
        {
            var result = await _supabaseClient
                .From<Customer>()
                .Get();

            return result.Models
                .Where(c => c.FullName.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public async Task<int> GetTotalCustomersCountAsync()
        {
            var result = await _supabaseClient
                .From<Customer>()
                .Get();

            return result.Models.Count;
        }
    }
}
