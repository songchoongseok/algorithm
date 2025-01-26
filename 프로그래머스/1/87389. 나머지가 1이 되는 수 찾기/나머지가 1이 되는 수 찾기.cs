using System;

public class Solution {
    public int solution(int n) {
        int answer = -1;
        
        
        for(int i=2; i<n; i++)
        {
            answer = Gcd(n - 1, i);
            if(answer > 1)
                break;
        }

        return answer;
    }
    
    public int Gcd(int a, int b)
    {
        while(b != 0)
        {
            int temp = a % b;
            a = b;
            b = temp;
        }
        
        return a;
    }
}

