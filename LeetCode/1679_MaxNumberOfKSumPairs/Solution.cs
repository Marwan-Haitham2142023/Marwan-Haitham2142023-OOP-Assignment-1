public int MaxOperations(int[] nums, int k)
{
    int n = nums.Length;
    int left = 0, right = 0;
    int operations = 0;
    while (left < right)
    {
        while (left < right && nums[left] + nums[right] > k)
        {
            right--;
        }
        while (left < right && nums[left] + nums[right] == k)
        {
            operations++;
            left++;
            right--;
        }
        while (left < right && nums[left] + nums[right] < k)
        {
            left++;
        }
    }