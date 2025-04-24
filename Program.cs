//*****************************************************************************
//** 2799. Count Complete Subarrays in an Array                     leetcode **
//*****************************************************************************

#define MAX_NUMS 100000 

typedef struct {
    int key;
    int count;
} Entry;

int countCompleteSubarrays(int* nums, int numsSize)
{
    int distinct[numsSize];
    int distinctCount = 0;

    for (int i = 0; i < numsSize; i++)
    {
        int found = 0;
        for (int j = 0; j < distinctCount; j++)
        {
            if (distinct[j] == nums[i])
            {
                found = 1;
                break;
            }
        }
        if (!found)
        {
            distinct[distinctCount++] = nums[i];
        }
    }

    int* counterKeys = (int*)malloc(sizeof(int) * numsSize);
    int* counterVals = (int*)calloc(numsSize, sizeof(int));
    int count = 0;
    int left = 0;
    int right = 0;
    int mapSize = 0;

    while (right < numsSize)
    {
        int val = nums[right];
        int found = 0;
        for (int i = 0; i < mapSize; i++)
        {
            if (counterKeys[i] == val)
            {
                counterVals[i] += 1;
                found = 1;
                break;
            }
        }
        if (!found)
        {
            counterKeys[mapSize] = val;
            counterVals[mapSize++] = 1;
        }

        while (mapSize == distinctCount)
        {
            count += numsSize - right;

            val = nums[left];
            for (int i = 0; i < mapSize; i++)
            {
                if (counterKeys[i] == val)
                {
                    counterVals[i] -= 1;
                    if (counterVals[i] == 0)
                    {
                        for (int j = i; j < mapSize - 1; j++)
                        {
                            counterKeys[j] = counterKeys[j + 1];
                            counterVals[j] = counterVals[j + 1];
                        }
                        mapSize--;
                    }
                    break;
                }
            }
            left++;
        }
        right++;
    }

    free(counterKeys);
    free(counterVals);

    return count;
}