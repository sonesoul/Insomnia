using System;
using System.Collections;

namespace Insomnia.Coroutines
{
    public class YieldInstruction
    {
        public IEnumerator WaitWhile(Func<bool> condition)
        {
            while (condition())
            {
                yield return null;
            }
        }

        public IEnumerator WaitForSeconds(float time)
        {
            while (time > 0)
            {
                time -= Time.Delta;
                yield return null;
            }
        }

        public IEnumerator Interpolate(Func<float, float> action)
        {
            float elapsed = 0;

            while (elapsed >= 0 && elapsed <= 1)
            {
                elapsed = action(elapsed);

                if (elapsed < 0 || elapsed > 1)
                {
                    yield return null;

                    if (elapsed > 1)
                        elapsed = 1;
                    else if (elapsed < 0) 
                        elapsed = 0;

                    action(elapsed);

                    break;
                }

                yield return null;
            }
        }
    }
}
