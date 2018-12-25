using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Ja;
using Lucene.Net.Analysis.Ja.Dict;
using Lucene.Net.Analysis.Ja.TokenAttributes;
using Lucene.Net.Analysis.TokenAttributes;

namespace KuromojiSample
{
    class Program
    {
        static void Main(string[] args)
        {
            const string s = "関西国際空港";
            Console.WriteLine($"対象の文字列:{s}");

            using (var reader = new StringReader(s))
            {
                Tokenizer tokenizer = new JapaneseTokenizer(reader, ReadDict(), false, JapaneseTokenizerMode.NORMAL);
                var tokenStreamComponents = new TokenStreamComponents(tokenizer, tokenizer);
                using (var tokenStream = tokenStreamComponents.TokenStream)
                {
                    // note:処理の実行前にResetを実行する必要がある
                    tokenStream.Reset();

                    while (tokenStream.IncrementToken())
                    {
                        Console.WriteLine("---");
                        Console.WriteLine(
                            $"ICharTermAttribute=>{tokenStream.GetAttribute<ICharTermAttribute>().ToString()}");

                        Console.WriteLine(
                            $"ITermToBytesRefAttribute#BytesRef=>{tokenStream.GetAttribute<ITermToBytesRefAttribute>().BytesRef}");

                        Console.WriteLine(
                            $"IOffsetAttribute#StartOffset=>{tokenStream.GetAttribute<IOffsetAttribute>().StartOffset}");
                        Console.WriteLine(
                            $"IOffsetAttribute#EndOffset=>{tokenStream.GetAttribute<IOffsetAttribute>().EndOffset}");

                        Console.WriteLine(
                            $"IPositionIncrementAttribute=>{tokenStream.GetAttribute<IPositionIncrementAttribute>().PositionIncrement}");
                        Console.WriteLine(
                            $"IPositionLengthAttribute=>{tokenStream.GetAttribute<IPositionLengthAttribute>().PositionLength}");

                        Console.WriteLine(
                            $"IBaseFormAttribute#GetBaseForm=>{tokenStream.GetAttribute<IBaseFormAttribute>().GetBaseForm()}");

                        Console.WriteLine(
                            $"IPartOfSpeechAttribute#GetPartOfSpeech=>{tokenStream.GetAttribute<IPartOfSpeechAttribute>().GetPartOfSpeech()}");

                        Console.WriteLine(
                            $"IReadingAttribute#GetReading=>{tokenStream.GetAttribute<IReadingAttribute>().GetReading()}");
                        Console.WriteLine(
                            $"IReadingAttribute#GetPronunciation=>{tokenStream.GetAttribute<IReadingAttribute>().GetPronunciation()}");

                        Console.WriteLine(
                            $"IInflectionAttribute#GetInflectionForm=>{tokenStream.GetAttribute<IInflectionAttribute>().GetInflectionForm()}");
                        Console.WriteLine(
                            $"IInflectionAttribute#GetInflectionType=>{tokenStream.GetAttribute<IInflectionAttribute>().GetInflectionType()}");

                        Console.WriteLine("---");
                    }
                }
            }
        }

        public static UserDictionary ReadDict()
        {
            TextReader reader = new StreamReader("userdict.txt", Encoding.UTF8);
            return new UserDictionary(reader);
        }
    }
}