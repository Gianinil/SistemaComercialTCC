using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SistemaComercial.UI;

public static class FormValidation
{
    private const string FormattingFlag = "__formatting";

    public static void AllowOnlyDigits(TextBox textBox, int maxLength)
    {
        textBox.MaxLength = maxLength;
        textBox.KeyPress += (_, e) =>
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        };
    }

    public static void ApplyCpfMask(TextBox textBox)
    {
        ApplyMaskedDigits(textBox, 11, FormatCpf);
    }

    public static void ApplyCnpjMask(TextBox textBox)
    {
        ApplyMaskedDigits(textBox, 14, FormatCnpj);
    }

    public static void ApplyPhoneMask(TextBox textBox)
    {
        ApplyMaskedDigits(textBox, 11, FormatPhone);
    }

    public static void LimitText(TextBox textBox, int maxLength)
    {
        textBox.MaxLength = maxLength;
    }

    public static bool IsCpfValid(string value)
    {
        string digits = OnlyDigits(value);
        if (digits.Length != 11 || AllDigitsAreEqual(digits))
        {
            return false;
        }

        int firstDigit = CalculateCpfDigit(digits, 9);
        int secondDigit = CalculateCpfDigit(digits, 10);

        return digits[9] == firstDigit.ToString()[0] && digits[10] == secondDigit.ToString()[0];
    }

    public static bool IsCnpjValid(string value)
    {
        string digits = OnlyDigits(value);
        if (digits.Length != 14 || AllDigitsAreEqual(digits))
        {
            return false;
        }

        int[] firstWeights = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] secondWeights = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int firstDigit = CalculateCnpjDigit(digits, firstWeights);
        int secondDigit = CalculateCnpjDigit(digits, secondWeights);

        return digits[12] == firstDigit.ToString()[0] && digits[13] == secondDigit.ToString()[0];
    }

    public static bool IsPhoneValid(string value)
    {
        string digits = OnlyDigits(value);
        return digits.Length == 10 || digits.Length == 11;
    }

    public static bool IsEmailValid(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        try
        {
            var address = new MailAddress(value.Trim());
            return address.Address == value.Trim();
        }
        catch
        {
            return false;
        }
    }

    public static string OnlyDigits(string value)
    {
        return Regex.Replace(value ?? string.Empty, @"\D", string.Empty);
    }

    private static void ApplyMaskedDigits(TextBox textBox, int maxDigits, Func<string, string> formatter)
    {
        textBox.MaxLength = formatter(new string('0', maxDigits)).Length;
        textBox.KeyPress += (_, e) =>
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        };

        textBox.TextChanged += (_, _) =>
        {
            if (Equals(textBox.Tag, FormattingFlag))
            {
                return;
            }

            string digits = OnlyDigits(textBox.Text);
            if (digits.Length > maxDigits)
            {
                digits = digits.Substring(0, maxDigits);
            }

            string formatted = formatter(digits);
            if (textBox.Text == formatted)
            {
                return;
            }

            textBox.Tag = FormattingFlag;
            textBox.Text = formatted;
            textBox.SelectionStart = textBox.Text.Length;
            textBox.Tag = null;
        };
    }

    private static string FormatCpf(string digits)
    {
        if (digits.Length <= 3)
        {
            return digits;
        }

        if (digits.Length <= 6)
        {
            return $"{digits[..3]}.{digits[3..]}";
        }

        if (digits.Length <= 9)
        {
            return $"{digits[..3]}.{digits[3..6]}.{digits[6..]}";
        }

        return $"{digits[..3]}.{digits[3..6]}.{digits[6..9]}-{digits[9..]}";
    }

    private static string FormatCnpj(string digits)
    {
        if (digits.Length <= 2)
        {
            return digits;
        }

        if (digits.Length <= 5)
        {
            return $"{digits[..2]}.{digits[2..]}";
        }

        if (digits.Length <= 8)
        {
            return $"{digits[..2]}.{digits[2..5]}.{digits[5..]}";
        }

        if (digits.Length <= 12)
        {
            return $"{digits[..2]}.{digits[2..5]}.{digits[5..8]}/{digits[8..]}";
        }

        return $"{digits[..2]}.{digits[2..5]}.{digits[5..8]}/{digits[8..12]}-{digits[12..]}";
    }

    private static string FormatPhone(string digits)
    {
        if (digits.Length <= 2)
        {
            return digits;
        }

        if (digits.Length <= 6)
        {
            return $"({digits[..2]}) {digits[2..]}";
        }

        if (digits.Length <= 10)
        {
            return $"({digits[..2]}) {digits[2..6]}-{digits[6..]}";
        }

        return $"({digits[..2]}) {digits[2..7]}-{digits[7..]}";
    }

    private static bool AllDigitsAreEqual(string digits)
    {
        return digits.All(digit => digit == digits[0]);
    }

    private static int CalculateCpfDigit(string digits, int length)
    {
        int sum = 0;

        for (int i = 0; i < length; i++)
        {
            sum += (digits[i] - '0') * (length + 1 - i);
        }

        int remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }

    private static int CalculateCnpjDigit(string digits, int[] weights)
    {
        int sum = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            sum += (digits[i] - '0') * weights[i];
        }

        int remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }
}
