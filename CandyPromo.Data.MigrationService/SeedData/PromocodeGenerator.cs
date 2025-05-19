using System.Text;

namespace CandyPromo.Data.MigrationService.SeedData;

/// <summary>
/// Генератор уникальных промокодов.
/// Источник информации: https://vk.com/@pro_promo_actions-generaciya-kodov-dlya-promo
/// </summary>
public static class PromocodeGenerator
{
    /// <summary>
    /// Алфавит промокода.
    /// Содержит только неконфликтующие буквы и цифры для того,
    /// чтобы покупатель не перепутал символы при чтении с упаковки.
    /// </summary>
    private const string ALPHABET = "ACEFGHKLMNPRSTWXY345679";

    /// <summary>
    /// Вероятность подбора промокода случайным образом.
    /// Чем меньше - тем лучше. Участвует в формуле расчета
    /// минимальной оптимальной длины промокода.
    /// </summary>
    private const double GUESSING_PROBABILITY = 0.1;

    /// <summary>
    /// Генерирует заданное количество промокодов.
    /// Алгоритм сам определяет длину промокода.
    /// </summary>
    public static List<Promocode> Generate(int count)
    {
        if (count < 1)
            throw new ArgumentException("Укажите положительное количество промокодов.");

        var promocodeLength = GetOptimalLength(count);

        var promocodes = new List<Promocode>();
        var random = new Random();
        var builder = new StringBuilder(promocodeLength);

        do
        {
            for (int i = promocodes.Count; i < count; i++)
            {
                for (int j = 0; j < promocodeLength; j++)
                {
                    builder.Append(ALPHABET[random.Next(0, ALPHABET.Length)]);
                }

                var promocode = new Promocode()
                {
                    Code = builder.ToString()
                };

                promocodes.Add(promocode);
                builder.Clear();
            }

            promocodes = promocodes.DistinctBy(p => p.Code).ToList();
        }
        while (promocodes.Count < count);

        return promocodes;
    }


    /// <summary>
    /// Вычисляет минимальную оптимальную длину промокода.
    /// Данная формула позволяет получить максимально короткие промокоды,
    /// не теряя в трудности случайного подбора.
    /// </summary>
    /// <param name="count"> Количество промокодов, участвующих в промоакции. </param>
    /// <returns> Длина промокода. </returns>
    private static int GetOptimalLength(int count)
    {
        return (int)Math.Ceiling(Math.Log(count * 100 / GUESSING_PROBABILITY, ALPHABET.Length));
    }
}
