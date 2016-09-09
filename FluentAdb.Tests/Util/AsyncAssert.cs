using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FluentAdb.Tests.Util
{
    public static class AsyncAssert
    {
        /// <summary>
        /// Assert that an async method fails due to a specific exception.
        /// </summary>
        /// <typeparam name="T">Exception type expected</typeparam>
        /// <param name="asyncDelegate">Test async delegate</param>
        public static async Task<T> Throws<T>(Func<Task> asyncDelegate) where T : Exception
        {
            try
            {
                await asyncDelegate();
                Assert.Fail("Expected exception of type: {0}", typeof(T));
            }
            catch (AssertionException)
            {
                throw;
            }
            catch (T ex)
            {
                // return this exception because it is expected
                return ex;
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected exception of type: {0} but was: {1}", typeof(T), ex);
            }

            throw new AssertionException(String.Format("Expected exception of type: {0}", typeof(T)));
        }
    }
}