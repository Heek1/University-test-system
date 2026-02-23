using University_test_system.Models;
using Microsoft.EntityFrameworkCore;

namespace University_test_system.Data;

// клас для початкового заповнення бази даних тестами, питаннями та відповідями, щоб не заповнювати вручну
public static class DataSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Tests.AnyAsync()) return;

        var tests = new List<Test>
        {
            // ─────────────────────────────────────────────
            // 1. МАТЕМАТИКА — Математичний аналіз
            // ─────────────────────────────────────────────
            new Test
            {
                Title = "Математичний аналіз: границі та похідні",
                SubjectId = 1,
                Level = "Середній",
                Time = 30,
                MaxAttempts = 3,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Яке значення має границя lim(x→0) sin(x)/x?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "0", IsTrue = false },
                            new Answer { Text = "1", IsTrue = true },
                            new Answer { Text = "∞", IsTrue = false },
                            new Answer { Text = "Не існує", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яка похідна функції f(x) = xⁿ за правилом степеня?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "nxⁿ", IsTrue = false },
                            new Answer { Text = "xⁿ⁻¹", IsTrue = false },
                            new Answer { Text = "n·xⁿ⁻¹", IsTrue = true },
                            new Answer { Text = "(n-1)·xⁿ", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яке значення має границя lim(x→∞) (1 + 1/x)ˣ?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "1", IsTrue = false },
                            new Answer { Text = "π", IsTrue = false },
                            new Answer { Text = "e ≈ 2.718", IsTrue = true },
                            new Answer { Text = "∞", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яка похідна функції f(x) = ln(x)?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "ln(x)/x", IsTrue = false },
                            new Answer { Text = "1/x", IsTrue = true },
                            new Answer { Text = "x·ln(x)", IsTrue = false },
                            new Answer { Text = "e^x", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке точка екстремуму функції?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Точка, де функція дорівнює нулю", IsTrue = false },
                            new Answer { Text = "Точка, де похідна не існує", IsTrue = false },
                            new Answer { Text = "Точка, де функція змінює монотонність", IsTrue = true },
                            new Answer { Text = "Точка розриву функції", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яка формула правила Лопіталя для невизначеності 0/0?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "lim f(x)/g(x) = lim f(x)·g(x)", IsTrue = false },
                            new Answer { Text = "lim f(x)/g(x) = lim f'(x)/g'(x)", IsTrue = true },
                            new Answer { Text = "lim f(x)/g(x) = f'(x)·g(x)", IsTrue = false },
                            new Answer { Text = "lim f(x)/g(x) = g'(x)/f'(x)", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Чому дорівнює ∫eˣ dx?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "eˣ/x + C", IsTrue = false },
                            new Answer { Text = "x·eˣ + C", IsTrue = false },
                            new Answer { Text = "eˣ + C", IsTrue = true },
                            new Answer { Text = "ln(eˣ) + C", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яка умова неперервності f(x) у точці x₀?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "f(x₀) = 0", IsTrue = false },
                            new Answer { Text = "lim(x→x₀) f(x) = f(x₀)", IsTrue = true },
                            new Answer { Text = "f'(x₀) існує", IsTrue = false },
                            new Answer { Text = "f(x₀) > 0", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що означає f''(x) > 0 на інтервалі?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Функція спадає", IsTrue = false },
                            new Answer { Text = "Функція опукла вниз (увігнута)", IsTrue = true },
                            new Answer { Text = "Функція опукла вгору", IsTrue = false },
                            new Answer { Text = "Функція має максимум", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яка похідна функції f(x) = sin(x)?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "-cos(x)", IsTrue = false },
                            new Answer { Text = "sin(x)", IsTrue = false },
                            new Answer { Text = "cos(x)", IsTrue = true },
                            new Answer { Text = "-sin(x)", IsTrue = false },
                        }
                    },
                }
            },

            // ─────────────────────────────────────────────
            // 2. МАТЕМАТИКА — Лінійна алгебра
            // ─────────────────────────────────────────────
            new Test
            {
                Title = "Лінійна алгебра: матриці та визначники",
                SubjectId = 1,
                Level = "Середній",
                Time = 25,
                MaxAttempts = 3,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Яка розмірність добутку матриць A(2×3) і B(3×4)?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "3×3", IsTrue = false },
                            new Answer { Text = "2×4", IsTrue = true },
                            new Answer { Text = "2×3", IsTrue = false },
                            new Answer { Text = "3×4", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Чому дорівнює визначник одиничної матриці?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "0", IsTrue = false },
                            new Answer { Text = "n", IsTrue = false },
                            new Answer { Text = "1", IsTrue = true },
                            new Answer { Text = "-1", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яка умова існування оберненої матриці A⁻¹?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Матриця є квадратною", IsTrue = false },
                            new Answer { Text = "det(A) ≠ 0", IsTrue = true },
                            new Answer { Text = "Всі елементи додатні", IsTrue = false },
                            new Answer { Text = "Матриця є симетричною", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке власний вектор матриці A?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Вектор, добуток якого з A дорівнює нулю", IsTrue = false },
                            new Answer { Text = "Вектор v, для якого Av = λv при деякому λ", IsTrue = true },
                            new Answer { Text = "Вектор з одиничною нормою", IsTrue = false },
                            new Answer { Text = "Стовпець матриці A", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Якщо det(A) = 0, то система Ax = b...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Має єдиний розв'язок", IsTrue = false },
                            new Answer { Text = "Не має розв'язків або має безліч розв'язків", IsTrue = true },
                            new Answer { Text = "Завжди має нульовий розв'язок", IsTrue = false },
                            new Answer { Text = "Має рівно два розв'язки", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке ранг матриці?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Кількість рядків матриці", IsTrue = false },
                            new Answer { Text = "Значення визначника", IsTrue = false },
                            new Answer { Text = "Максимальна кількість лінійно незалежних рядків (стовпців)", IsTrue = true },
                            new Answer { Text = "Кількість ненульових елементів", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яке твердження про (Aᵀ)ᵀ є вірним?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "(Aᵀ)ᵀ = -A", IsTrue = false },
                            new Answer { Text = "(Aᵀ)ᵀ = Aᵀ", IsTrue = false },
                            new Answer { Text = "(Aᵀ)ᵀ = A", IsTrue = true },
                            new Answer { Text = "(Aᵀ)ᵀ = A⁻¹", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Скалярний добуток a⃗ · b⃗ = 0 означає, що вектори...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Паралельні", IsTrue = false },
                            new Answer { Text = "Колінеарні", IsTrue = false },
                            new Answer { Text = "Ортогональні", IsTrue = true },
                            new Answer { Text = "Один із них нульовий", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Теорема Кронекера-Капеллі: система сумісна тоді і тільки тоді, коли...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "det(A) ≠ 0", IsTrue = false },
                            new Answer { Text = "rank(A) = rank(A|b)", IsTrue = true },
                            new Answer { Text = "rank(A) = n", IsTrue = false },
                            new Answer { Text = "Система однорідна", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яке перетворення рядків не змінює визначник?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Множення рядка на константу", IsTrue = false },
                            new Answer { Text = "Перестановка двох рядків", IsTrue = false },
                            new Answer { Text = "Додавання до рядка лінійної комбінації інших рядків", IsTrue = true },
                            new Answer { Text = "Транспонування", IsTrue = false },
                        }
                    },
                }
            },

            // ─────────────────────────────────────────────
            // 3. ФІЗИКА — Класична механіка
            // ─────────────────────────────────────────────
            new Test
            {
                Title = "Класична механіка: закони Ньютона та динаміка",
                SubjectId = 2,
                Level = "Середній",
                Time = 30,
                MaxAttempts = 3,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Що стверджує перший закон Ньютона?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "F = ma", IsTrue = false },
                            new Answer { Text = "Тіло рівноприскорено рухається при наявності сили", IsTrue = false },
                            new Answer { Text = "Тіло зберігає стан спокою або рівномірного руху, якщо рівнодійна сил дорівнює нулю", IsTrue = true },
                            new Answer { Text = "Дія дорівнює протидії", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Імпульс тіла — це...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Добуток маси на прискорення", IsTrue = false },
                            new Answer { Text = "Добуток сили на час", IsTrue = false },
                            new Answer { Text = "Добуток маси на швидкість", IsTrue = true },
                            new Answer { Text = "Кінетична енергія тіла", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Закон збереження механічної енергії виконується, якщо...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "На тіло діє сила тяжіння", IsTrue = false },
                            new Answer { Text = "Діють лише консервативні сили", IsTrue = true },
                            new Answer { Text = "Тіло рухається з постійною швидкістю", IsTrue = false },
                            new Answer { Text = "Маса тіла не змінюється", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Одиниця вимірювання роботи в системі СІ:",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Ватт (Вт)", IsTrue = false },
                            new Answer { Text = "Ньютон (Н)", IsTrue = false },
                            new Answer { Text = "Джоуль (Дж)", IsTrue = true },
                            new Answer { Text = "Паскаль (Па)", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Момент інерції точкової маси m на відстані r від осі:",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "mr", IsTrue = false },
                            new Answer { Text = "m/r²", IsTrue = false },
                            new Answer { Text = "mr²", IsTrue = true },
                            new Answer { Text = "2mr", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Рівняння обертального руху твердого тіла:",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "M = Iα", IsTrue = true },
                            new Answer { Text = "M = mv²", IsTrue = false },
                            new Answer { Text = "F = Iω", IsTrue = false },
                            new Answer { Text = "M = Fr²", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Тіло кинуто горизонтально з висоти h. Час падіння:",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "t = v₀/g", IsTrue = false },
                            new Answer { Text = "t = √(2h/g)", IsTrue = true },
                            new Answer { Text = "t = h/v₀", IsTrue = false },
                            new Answer { Text = "t = 2h/g", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Як змінюється сила гравітації при збільшенні відстані вдвічі?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Зменшується вдвічі", IsTrue = false },
                            new Answer { Text = "Зменшується в 4 рази", IsTrue = true },
                            new Answer { Text = "Зменшується в 8 разів", IsTrue = false },
                            new Answer { Text = "Не змінюється", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Принцип суперпозиції в механіці означає...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Рівнодійна дорівнює добутку сил", IsTrue = false },
                            new Answer { Text = "Результуючий вплив декількох сил дорівнює їх векторній сумі", IsTrue = true },
                            new Answer { Text = "Сили завжди попарно рівні", IsTrue = false },
                            new Answer { Text = "Рух тіла не залежить від сил", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке центр мас системи тіл?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Геометричний центр системи", IsTrue = false },
                            new Answer { Text = "Точка, де зосереджена вся маса при обчисленні трансляційного руху", IsTrue = true },
                            new Answer { Text = "Точка з найбільшою масою", IsTrue = false },
                            new Answer { Text = "Точка мінімальної потенціальної енергії", IsTrue = false },
                        }
                    },
                }
            },

            // ─────────────────────────────────────────────
            // 4. ФІЗИКА — Електромагнетизм
            // ─────────────────────────────────────────────
            new Test
            {
                Title = "Електромагнетизм: поля та закони",
                SubjectId = 2,
                Level = "Складний",
                Time = 35,
                MaxAttempts = 3,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Закон Кулона описує силу взаємодії між...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Двома магнітними полюсами", IsTrue = false },
                            new Answer { Text = "Двома точковими електричними зарядами", IsTrue = true },
                            new Answer { Text = "Зарядом і магнітним полем", IsTrue = false },
                            new Answer { Text = "Двома провідниками зі струмом", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що стверджує теорема Гауса для електричного поля?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Потік E через замкнену поверхню дорівнює нулю", IsTrue = false },
                            new Answer { Text = "Потік E пропорційний сумарному заряду всередині поверхні", IsTrue = true },
                            new Answer { Text = "Електричне поле завжди потенціальне", IsTrue = false },
                            new Answer { Text = "Поле поза провідником відсутнє", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Закон Фарадея: ЕРС індукції дорівнює...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Магнітному потоку через контур", IsTrue = false },
                            new Answer { Text = "Швидкості зміни магнітного потоку з від'ємним знаком", IsTrue = true },
                            new Answer { Text = "Добутку індуктивності на струм", IsTrue = false },
                            new Answer { Text = "Силі струму в контурі", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Одиниця вимірювання електричної ємності:",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Генрі (Гн)", IsTrue = false },
                            new Answer { Text = "Вебер (Вб)", IsTrue = false },
                            new Answer { Text = "Фарад (Ф)", IsTrue = true },
                            new Answer { Text = "Тесла (Тл)", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Сила Лоренца на заряд q зі швидкістю v у полі B:",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "F = qE", IsTrue = false },
                            new Answer { Text = "F = qvB·sin(α)", IsTrue = true },
                            new Answer { Text = "F = q²vB", IsTrue = false },
                            new Answer { Text = "F = mv²/r", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Які рівняння описують електромагнітне поле?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Рівняння Шредінгера", IsTrue = false },
                            new Answer { Text = "Рівняння Максвела", IsTrue = true },
                            new Answer { Text = "Рівняння Ньютона", IsTrue = false },
                            new Answer { Text = "Рівняння Больцмана", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "При послідовному з'єднанні конденсаторів...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Загальна ємність збільшується", IsTrue = false },
                            new Answer { Text = "Загальна ємність менша за найменшу з ємностей", IsTrue = true },
                            new Answer { Text = "Напруга на кожному однакова", IsTrue = false },
                            new Answer { Text = "Заряд на кожному різний", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Правило Ленца означає, що...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Індукований струм збільшує магнітний потік", IsTrue = false },
                            new Answer { Text = "Індукований струм протидіє зміні потоку, що його спричинила", IsTrue = true },
                            new Answer { Text = "ЕРС завжди позитивна", IsTrue = false },
                            new Answer { Text = "Індукція виникає лише в котушках", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке діелектрична проникність середовища ε?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Відношення заряду до напруги", IsTrue = false },
                            new Answer { Text = "Величина, що показує у скільки разів поле в середовищі слабше ніж у вакуумі", IsTrue = true },
                            new Answer { Text = "Опір діелектрика", IsTrue = false },
                            new Answer { Text = "Швидкість поширення поля", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке ЕРС джерела струму?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Опір джерела", IsTrue = false },
                            new Answer { Text = "Напруга на клемах при розімкненому колі", IsTrue = false },
                            new Answer { Text = "Робота стороннього поля з переміщення одиничного заряду всередині джерела", IsTrue = true },
                            new Answer { Text = "Потужність у зовнішньому колі", IsTrue = false },
                        }
                    },
                }
            },

            // ─────────────────────────────────────────────
            // 5. ХІМІЯ — Загальна хімія
            // ─────────────────────────────────────────────
            new Test
            {
                Title = "Загальна хімія: будова речовини та хімічні зв'язки",
                SubjectId = 3,
                Level = "Середній",
                Time = 25,
                MaxAttempts = 3,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Принцип Паулі стверджує, що...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Електрони заповнюють орбіталі з найменшою енергією", IsTrue = false },
                            new Answer { Text = "В атомі не може бути двох електронів з однаковим набором всіх чотирьох квантових чисел", IsTrue = true },
                            new Answer { Text = "Спін електрона завжди +1/2", IsTrue = false },
                            new Answer { Text = "Орбіталі заповнюються по одному перед спаруванням", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Який тип зв'язку утворюється між Na і Cl?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Ковалентний неполярний", IsTrue = false },
                            new Answer { Text = "Ковалентний полярний", IsTrue = false },
                            new Answer { Text = "Іонний", IsTrue = true },
                            new Answer { Text = "Металічний", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яка геометрична форма молекули CH₄?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Плоска квадратна", IsTrue = false },
                            new Answer { Text = "Лінійна", IsTrue = false },
                            new Answer { Text = "Тетраедрична", IsTrue = true },
                            new Answer { Text = "Кутова", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке ентальпія реакції (ΔH)?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Зміна ентропії системи", IsTrue = false },
                            new Answer { Text = "Теплота реакції при постійному тиску", IsTrue = true },
                            new Answer { Text = "Вільна енергія Гіббса", IsTrue = false },
                            new Answer { Text = "Швидкість хімічної реакції", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Принцип Ле Шательє стверджує, що...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Рівновага завжди зміщується у бік реагентів при нагріванні", IsTrue = false },
                            new Answer { Text = "При зовнішньому впливі рівновага зміщується так, щоб послабити цей вплив", IsTrue = true },
                            new Answer { Text = "Константа рівноваги не залежить від температури", IsTrue = false },
                            new Answer { Text = "Тиск не впливає на рівновагу", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке рН розчину?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Концентрація гідроксид-іонів", IsTrue = false },
                            new Answer { Text = "Від'ємний десятковий логарифм концентрації іонів H⁺", IsTrue = true },
                            new Answer { Text = "Молярна концентрація кислоти", IsTrue = false },
                            new Answer { Text = "Ступінь дисоціації електроліту", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Закон Авогадро: в рівних об'ємах різних газів за однакових умов...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Однакова маса", IsTrue = false },
                            new Answer { Text = "Однаковий тиск", IsTrue = false },
                            new Answer { Text = "Однакова кількість молекул", IsTrue = true },
                            new Answer { Text = "Однакова молярна маса", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке електронегативність елемента?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Кількість електронів на зовнішньому рівні", IsTrue = false },
                            new Answer { Text = "Здатність атома притягувати електронну пару у зв'язку", IsTrue = true },
                            new Answer { Text = "Заряд ядра атома", IsTrue = false },
                            new Answer { Text = "Мінімальна енергія іонізації", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке гібридизація атомних орбіталей?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Перенесення електронів між атомами", IsTrue = false },
                            new Answer { Text = "Змішування орбіталей різних типів для утворення рівноцінних орбіталей", IsTrue = true },
                            new Answer { Text = "Утворення подвійного зв'язку", IsTrue = false },
                            new Answer { Text = "Зміна заряду ядра", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яка модель описує атом Бора?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Електрони розподілені рівномірно по об'єму атома", IsTrue = false },
                            new Answer { Text = "Електрони рухаються стаціонарними орбітами не випромінюючи енергію", IsTrue = true },
                            new Answer { Text = "Електрони у хмарі без чітких орбіт", IsTrue = false },
                            new Answer { Text = "Ядро займає весь об'єм атома", IsTrue = false },
                        }
                    },
                }
            },

            // ─────────────────────────────────────────────
            // 6. БІОЛОГІЯ — Молекулярна біологія
            // ─────────────────────────────────────────────
            new Test
            {
                Title = "Молекулярна біологія: ДНК, РНК та синтез білків",
                SubjectId = 4,
                Level = "Складний",
                Time = 30,
                MaxAttempts = 3,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Яка структура ДНК запропонована Уотсоном і Криком?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Одноланцюгова лінійна молекула", IsTrue = false },
                            new Answer { Text = "Подвійна спіраль з комплементарними ланцюгами", IsTrue = true },
                            new Answer { Text = "Кругова трьохланцюгова молекула", IsTrue = false },
                            new Answer { Text = "Розгалужена полімерна структура", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "В ДНК аденін (A) комплементарний до...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Цитозину (C)", IsTrue = false },
                            new Answer { Text = "Гуаніну (G)", IsTrue = false },
                            new Answer { Text = "Тиміну (T)", IsTrue = true },
                            new Answer { Text = "Урацилу (U)", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке транскрипція?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Синтез білка на рибосомі", IsTrue = false },
                            new Answer { Text = "Синтез РНК на матриці ДНК", IsTrue = true },
                            new Answer { Text = "Реплікація ДНК", IsTrue = false },
                            new Answer { Text = "Процес мутації гена", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Виродженість генетичного коду означає...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Деякі кодони не кодують жодної амінокислоти", IsTrue = false },
                            new Answer { Text = "Одну амінокислоту можуть кодувати кілька різних кодонів", IsTrue = true },
                            new Answer { Text = "Один кодон кодує кілька амінокислот", IsTrue = false },
                            new Answer { Text = "Код різний у різних організмів", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яка роль рибосом у клітині?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Синтез ДНК", IsTrue = false },
                            new Answer { Text = "Транспорт речовин", IsTrue = false },
                            new Answer { Text = "Синтез білків (трансляція)", IsTrue = true },
                            new Answer { Text = "Розщеплення РНК", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Мутація зсуву рамки зчитування — це...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Заміна одного нуклеотиду на інший", IsTrue = false },
                            new Answer { Text = "Вставка або делеція нуклеотидів кількість яких не кратна трьом", IsTrue = true },
                            new Answer { Text = "Інверсія ділянки хромосоми", IsTrue = false },
                            new Answer { Text = "Транслокація між хромосомами", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Закон Менделя про незалежне розщеплення стосується генів, що...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Знаходяться в одній хромосомі", IsTrue = false },
                            new Answer { Text = "Знаходяться в різних парах хромосом і успадковуються незалежно", IsTrue = true },
                            new Answer { Text = "Кодують один білок", IsTrue = false },
                            new Answer { Text = "Зчеплені зі статтю", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке ПЛР (полімеразна ланцюгова реакція)?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Метод розщеплення ДНК", IsTrue = false },
                            new Answer { Text = "Метод ампліфікації ділянки ДНК in vitro", IsTrue = true },
                            new Answer { Text = "Метод синтезу білків поза клітиною", IsTrue = false },
                            new Answer { Text = "Техніка електрофорезу білків", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке апоптоз?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Некроз клітини внаслідок пошкодження", IsTrue = false },
                            new Answer { Text = "Неконтрольоване поділення клітини", IsTrue = false },
                            new Answer { Text = "Програмована клітинна загибель", IsTrue = true },
                            new Answer { Text = "Диференціація стовбурових клітин", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Оперон у прокаріотів (модель Жакоба-Моно) — це...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Сукупність усіх генів клітини", IsTrue = false },
                            new Answer { Text = "Структурна одиниця хромосоми", IsTrue = false },
                            new Answer { Text = "Промотор + оператор + структурні гени як одиниця регуляції транскрипції", IsTrue = true },
                            new Answer { Text = "Органела, що синтезує РНК", IsTrue = false },
                        }
                    },
                }
            },

            // ─────────────────────────────────────────────
            // 7. ІСТОРІЯ — Новітня історія України
            // ─────────────────────────────────────────────
            new Test
            {
                Title = "Новітня історія України (XX–XXI ст.)",
                SubjectId = 5,
                Level = "Легкий",
                Time = 20,
                MaxAttempts = 1,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Коли проголошено незалежність України?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "24 серпня 1990 р.", IsTrue = false },
                            new Answer { Text = "24 серпня 1991 р.", IsTrue = true },
                            new Answer { Text = "1 грудня 1991 р.", IsTrue = false },
                            new Answer { Text = "16 липня 1990 р.", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яка трагедія сталась 26 квітня 1986 року?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Голодомор", IsTrue = false },
                            new Answer { Text = "Катастрофа на Чорнобильській АЕС", IsTrue = true },
                            new Answer { Text = "Вибух на Балаклавській підводній базі", IsTrue = false },
                            new Answer { Text = "Землетрус у Вірменії", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Голодомор 1932–1933 рр. — це...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Природна посуха", IsTrue = false },
                            new Answer { Text = "Штучно організований радянським режимом голод із масовою загибеллю українців", IsTrue = true },
                            new Answer { Text = "Наслідок Першої світової", IsTrue = false },
                            new Answer { Text = "Наслідок Громадянської війни", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Конституція незалежної України прийнята...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "1 грудня 1991 р.", IsTrue = false },
                            new Answer { Text = "24 серпня 1991 р.", IsTrue = false },
                            new Answer { Text = "28 червня 1996 р.", IsTrue = true },
                            new Answer { Text = "16 липня 1990 р.", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке Помаранчева революція?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Повстання проти польської влади у XIX ст.", IsTrue = false },
                            new Answer { Text = "Масові протести 2004 р. проти фальсифікації президентських виборів", IsTrue = true },
                            new Answer { Text = "Антикомуністичне повстання 1991 р.", IsTrue = false },
                            new Answer { Text = "Військовий переворот 1994 р.", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке Революція Гідності (Євромайдан)?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Виборча кампанія 2010 р.", IsTrue = false },
                            new Answer { Text = "Масові протести 2013–2014 рр. проти відмови від євроінтеграції та корупції", IsTrue = true },
                            new Answer { Text = "Референдум про незалежність 1991 р.", IsTrue = false },
                            new Answer { Text = "Підписання Будапештського меморандуму", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Будапештський меморандум 1994 р. передбачав...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Вступ України до НАТО", IsTrue = false },
                            new Answer { Text = "Гарантії безпеки в обмін на відмову від ядерної зброї", IsTrue = true },
                            new Answer { Text = "Угоду про вільну торгівлю з ЄС", IsTrue = false },
                            new Answer { Text = "Визнання Криму частиною Росії", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Коли Росія окупувала Крим?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Лютий–березень 2013 р.", IsTrue = false },
                            new Answer { Text = "Лютий–березень 2014 р.", IsTrue = true },
                            new Answer { Text = "Квітень 2015 р.", IsTrue = false },
                            new Answer { Text = "Серпень 2008 р.", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Дата початку повномасштабного вторгнення Росії в Україну:",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "24 лютого 2021 р.", IsTrue = false },
                            new Answer { Text = "24 лютого 2022 р.", IsTrue = true },
                            new Answer { Text = "1 березня 2022 р.", IsTrue = false },
                            new Answer { Text = "16 лютого 2022 р.", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яким документом проголошено суверенітет УРСР 16 липня 1990 р.?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Конституція України", IsTrue = false },
                            new Answer { Text = "Декларація про державний суверенітет України", IsTrue = true },
                            new Answer { Text = "Акт проголошення незалежності", IsTrue = false },
                            new Answer { Text = "Будапештський меморандум", IsTrue = false },
                        }
                    },
                }
            },

            // ─────────────────────────────────────────────
            // 8. ІНОЗЕМНА МОВА — Academic English
            // ─────────────────────────────────────────────
            new Test
            {
                Title = "Academic English: Grammar and Vocabulary",
                SubjectId = 6,
                Level = "Середній",
                Time = 25,
                MaxAttempts = 3,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Choose the correct form: 'The data ___ been analysed thoroughly.'",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "has", IsTrue = true },
                            new Answer { Text = "have", IsTrue = false },
                            new Answer { Text = "is", IsTrue = false },
                            new Answer { Text = "are", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Which sentence uses the passive voice correctly?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "We conducted the experiment.", IsTrue = false },
                            new Answer { Text = "The experiment was conducted under controlled conditions.", IsTrue = true },
                            new Answer { Text = "They are conducting the experiment.", IsTrue = false },
                            new Answer { Text = "The experiment conducts itself.", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "What does 'substantiate' mean?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "To contradict a claim", IsTrue = false },
                            new Answer { Text = "To provide evidence to support a claim", IsTrue = true },
                            new Answer { Text = "To summarise an argument", IsTrue = false },
                            new Answer { Text = "To question the methodology", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Which transition word indicates contrast?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Furthermore", IsTrue = false },
                            new Answer { Text = "Consequently", IsTrue = false },
                            new Answer { Text = "Nevertheless", IsTrue = true },
                            new Answer { Text = "Similarly", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Choose the correct preposition: 'This study focuses ___ climate change.'",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "in", IsTrue = false },
                            new Answer { Text = "about", IsTrue = false },
                            new Answer { Text = "on", IsTrue = true },
                            new Answer { Text = "at", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "What is a thesis statement?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "A summary of all sources used", IsTrue = false },
                            new Answer { Text = "A sentence presenting the main argument of the paper", IsTrue = true },
                            new Answer { Text = "The concluding paragraph", IsTrue = false },
                            new Answer { Text = "A list of research questions", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Which word is a synonym for 'significant'?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Trivial", IsTrue = false },
                            new Answer { Text = "Ambiguous", IsTrue = false },
                            new Answer { Text = "Substantial", IsTrue = true },
                            new Answer { Text = "Marginal", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "What does 'et al.' mean in academic citations?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "And the following pages", IsTrue = false },
                            new Answer { Text = "And others (referring to additional authors)", IsTrue = true },
                            new Answer { Text = "In the same place", IsTrue = false },
                            new Answer { Text = "Compare with", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Identify the correct Type 2 conditional (unreal present):",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "If I study, I will pass.", IsTrue = false },
                            new Answer { Text = "If I had studied, I would have passed.", IsTrue = false },
                            new Answer { Text = "If I studied more, I would pass.", IsTrue = true },
                            new Answer { Text = "If I study, I would pass.", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "What is plagiarism?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Using too many sources", IsTrue = false },
                            new Answer { Text = "Presenting someone else's work as your own without attribution", IsTrue = true },
                            new Answer { Text = "Writing an overly long essay", IsTrue = false },
                            new Answer { Text = "Citing sources incorrectly", IsTrue = false },
                        }
                    },
                }
            },

            // ─────────────────────────────────────────────
            // 9. МАТЕМАТИКА — Теорія ймовірностей
            // ─────────────────────────────────────────────
            new Test
            {
                Title = "Теорія ймовірностей та математична статистика",
                SubjectId = 1,
                Level = "Складний",
                Time = 35,
                MaxAttempts = 3,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Яка аксіоматика покладена в основу теорії ймовірностей?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Аксіоматика Евкліда", IsTrue = false },
                            new Answer { Text = "Аксіоматика Колмогорова (1933)", IsTrue = true },
                            new Answer { Text = "Аксіоматика Байєса", IsTrue = false },
                            new Answer { Text = "Аксіоматика фон Мізеса", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Формула Байєса використовується для...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Обчислення дисперсії", IsTrue = false },
                            new Answer { Text = "Оновлення ймовірності гіпотези після отримання нових даних", IsTrue = true },
                            new Answer { Text = "Знаходження математичного сподівання", IsTrue = false },
                            new Answer { Text = "Перевірки нормальності розподілу", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Центральна гранична теорема стверджує, що...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Будь-яка величина має нормальний розподіл", IsTrue = false },
                            new Answer { Text = "Сума великої кількості незалежних однаково розподілених величин наближається до нормального розподілу", IsTrue = true },
                            new Answer { Text = "Середнє значення завжди дорівнює медіані", IsTrue = false },
                            new Answer { Text = "Дисперсія суми завжди дорівнює сумі дисперсій", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Математичне сподівання дискретної величини X — це...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Найбільш ймовірне значення X", IsTrue = false },
                            new Answer { Text = "Середнє значення у вибірці", IsTrue = false },
                            new Answer { Text = "E(X) = Σ xᵢpᵢ", IsTrue = true },
                            new Answer { Text = "Різниця між max і min значеннями X", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Умова незалежності подій A і B:",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "P(A∩B) = P(A) + P(B)", IsTrue = false },
                            new Answer { Text = "P(A∩B) = P(A)·P(B)", IsTrue = true },
                            new Answer { Text = "P(A|B) = P(B|A)", IsTrue = false },
                            new Answer { Text = "P(A∪B) = 1", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке p-value у статистичних тестах?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Ймовірність того, що гіпотеза правильна", IsTrue = false },
                            new Answer { Text = "Ймовірність отримати настільки екстремальний результат якщо нульова гіпотеза вірна", IsTrue = true },
                            new Answer { Text = "Рівень значущості тесту", IsTrue = false },
                            new Answer { Text = "Потужність статистичного тесту", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яке зі значень дисперсії не може бути реальним?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "D(X) = 0", IsTrue = false },
                            new Answer { Text = "D(X) = 0.001", IsTrue = false },
                            new Answer { Text = "D(X) = -5", IsTrue = true },
                            new Answer { Text = "D(X) = 1000", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Біноміальний розподіл B(n, p) описує...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Час очікування між подіями", IsTrue = false },
                            new Answer { Text = "Кількість успіхів у n незалежних випробуваннях з ймовірністю p", IsTrue = true },
                            new Answer { Text = "Розподіл неперервної величини", IsTrue = false },
                            new Answer { Text = "Суму нормально розподілених величин", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке коефіцієнт кореляції Пірсона?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Відношення дисперсій двох вибірок", IsTrue = false },
                            new Answer { Text = "Міра лінійного зв'язку між двома змінними від -1 до 1", IsTrue = true },
                            new Answer { Text = "Середньоквадратичне відхилення", IsTrue = false },
                            new Answer { Text = "Рівень значущості тесту", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Закон великих чисел стверджує, що...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Велика вибірка завжди нормально розподілена", IsTrue = false },
                            new Answer { Text = "При збільшенні кількості випробувань відносна частота наближається до ймовірності події", IsTrue = true },
                            new Answer { Text = "Математичне сподівання дорівнює медіані", IsTrue = false },
                            new Answer { Text = "Дисперсія зменшується вдвічі при збільшенні вибірки вдвічі", IsTrue = false },
                        }
                    },
                }
            },

            // ─────────────────────────────────────────────
            // 10. БІОЛОГІЯ — Клітинна біологія
            // ─────────────────────────────────────────────
            new Test
            {
                Title = "Клітинна біологія: структура та функції клітини",
                SubjectId = 4,
                Level = "Середній",
                Time = 25,
                MaxAttempts = 3,
                Questions = new List<Question>
                {
                    new Question
                    {
                        Title = "Яка основна відмінність еукаріота від прокаріота?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Наявність клітинної мембрани", IsTrue = false },
                            new Answer { Text = "Наявність оформленого ядра з ядерною оболонкою", IsTrue = true },
                            new Answer { Text = "Наявність рибосом", IsTrue = false },
                            new Answer { Text = "Здатність до поділу", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яка функція мітохондрій?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Синтез білків", IsTrue = false },
                            new Answer { Text = "Фотосинтез", IsTrue = false },
                            new Answer { Text = "Синтез АТФ шляхом клітинного дихання", IsTrue = true },
                            new Answer { Text = "Транспорт речовин між органелами", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яку роль відіграє комплекс Гольджі?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Виробництво енергії", IsTrue = false },
                            new Answer { Text = "Сортування, модифікація та пакування білків для секреції", IsTrue = true },
                            new Answer { Text = "Синтез ліпідів мембрани", IsTrue = false },
                            new Answer { Text = "Збереження генетичної інформації", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке осмос?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Активний транспорт іонів", IsTrue = false },
                            new Answer { Text = "Дифузія розчинених речовин через мембрану", IsTrue = false },
                            new Answer { Text = "Переміщення розчинника через напівпроникну мембрану з меншої концентрації до більшої", IsTrue = true },
                            new Answer { Text = "Поглинання великих молекул ендоцитозом", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Які стадії включає клітинний цикл?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Тільки мітоз і цитокінез", IsTrue = false },
                            new Answer { Text = "Інтерфаза (G1, S, G2) та мітотична фаза", IsTrue = true },
                            new Answer { Text = "Лише профаза, метафаза, анафаза, телофаза", IsTrue = false },
                            new Answer { Text = "Синтез ДНК і поділ ядра", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що відбувається під час S-фази клітинного циклу?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Ріст клітини та синтез білків", IsTrue = false },
                            new Answer { Text = "Реплікація ДНК", IsTrue = true },
                            new Answer { Text = "Поділ хроматид", IsTrue = false },
                            new Answer { Text = "Формування веретена поділу", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Ліпідний бішар клітинної мембрани — це...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Два шари білків навколо клітини", IsTrue = false },
                            new Answer { Text = "Подвійний шар фосфоліпідів з гідрофільними головками назовні та гідрофобними хвостами всередину", IsTrue = true },
                            new Answer { Text = "Целюлозна оболонка рослинних клітин", IsTrue = false },
                            new Answer { Text = "Шар полісахаридів на поверхні клітини", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Яка роль лізосом у клітині?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Синтез АТФ", IsTrue = false },
                            new Answer { Text = "Фотосинтез", IsTrue = false },
                            new Answer { Text = "Розщеплення макромолекул та пошкоджених органел гідролітичними ферментами", IsTrue = true },
                            new Answer { Text = "Синтез рибосомних РНК", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Теорія ендосимбіогенезу (Маргуліс) стверджує, що...",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Віруси виникли з бактерій", IsTrue = false },
                            new Answer { Text = "Мітохондрії і хлоропласти виникли з прокаріотів у симбіозі з клітиною-господарем", IsTrue = true },
                            new Answer { Text = "Клітини виникли спонтанно", IsTrue = false },
                            new Answer { Text = "Клітинна мембрана складається з білків", IsTrue = false },
                        }
                    },
                    new Question
                    {
                        Title = "Що таке ендоплазматичний ретикулум (ЕР)?",
                        Answers = new List<Answer>
                        {
                            new Answer { Text = "Органела, що руйнує відпрацьовані молекули", IsTrue = false },
                            new Answer { Text = "Мережа мембранних канальців для транспорту та модифікації білків і ліпідів", IsTrue = true },
                            new Answer { Text = "Комплекс, що синтезує АТФ", IsTrue = false },
                            new Answer { Text = "Клітинний центр поділу", IsTrue = false },
                        }
                    },
                }
            },
        };

        await context.Tests.AddRangeAsync(tests);
        await context.SaveChangesAsync();

        // Призначаємо всі тести всім факультетам
        var facultyIds = await context.Faculties.Select(f => f.Id).ToListAsync();
        var testIds = await context.Tests.Select(t => t.Id).ToListAsync();

        foreach (var testId in testIds)
        {
            foreach (var facultyId in facultyIds)
            {
                context.TestFaculties.Add(new TestFaculty
                {
                    TestId = testId,
                    FacultyId = facultyId
                });
            }
        }

        await context.SaveChangesAsync();
    }
}