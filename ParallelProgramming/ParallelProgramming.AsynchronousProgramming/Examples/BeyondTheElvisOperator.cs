namespace ParallelProgramming.AsynchronousProgramming.Examples.BeyondTheElvisOperator
{
    public class Person
    {
        public Address Address { get; set; }
    }
    public class Address
    {
        public string PostCode { get; set; }
    }

    public static class Maybe
    {
        public static TResult With<TInput, TResult>(this TInput input,
            Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            if (input is null)
                return null;
            else 
                return evaluator(input);
        }

        public static TInput If<TInput>(this TInput input,
            Func<TInput, bool> evaluator)
            where TInput : class
        {
            if (input is null) return null;
            return evaluator(input) ? input : null;
        }

        public static TInput Do<TInput>(this TInput input,
            Action<TInput> action)
            where TInput : class
        {
            if (input is null) return null;
            action(input);
            return input;
        }

        public static TResult Return<TInput, TResult>(this TInput input,
            Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            if (input is null) return failureValue;
            return evaluator(input);
        }

        public static TResult WithValue<TInput, TResult>(this TInput input,
            Func<TInput, TResult> evaluator)
            where TInput : struct
        {
            return evaluator(input);
        }
    }

    public class BeyondTheElvisOperator
    {
        public void MyMethod(Person p)
        {
            //string postcode = "UNKNOWN";
            //if(p != null && p.Address != null && p.Address.PostCode != null)
            //    postcode = p.Address.PostCode;
            //postcode = p?.Address?.PostCode;

            //string postcode;
            //if(p is not null)
            //{
            //    if(HasMedicalRecord(p) && p.Address is not null)
            //    {
            //        CheckAddress(p.Address);
            //        if (p.Address.PostCode is not null)
            //            postcode = p.Address.PostCode.ToString();
            //        else
            //            postcode = "UNKNOWN";
            //    }
            //}

            string postcode = p.With(x => x.Address).With(x => x.PostCode);

            postcode = p // If something is null in context then subsequent calls do not happen
                .If(HasMedicalRecord)
                .With(x => x.Address)
                .Do(CheckAddress)
                .Return(x => x.PostCode, "UNKNOWN");
        }

        bool HasMedicalRecord(Person arg)
        {
            throw new NotImplementedException();
        }

        void CheckAddress(Address obj)
        {
            throw new NotImplementedException();
        }

        public static void Start(string[] args)
        {

        }
    }
}
