namespace Result
{
    internal class Result
    {
        //전투 시작 화면
        static void DisplayResult(string[] args)
        {
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            //승리 or 패배 가져오기
            Console.WriteLine();
            Console.WriteLine($"{//레벨 체력 체력 변경사항}");
            Console.WriteLine();

    
            int input = CheckValidInput(0, 0);
            switch (input)
                {
                    case 0:
                    DisplayGameIntro();//끝나면 인트로 화면으로
                    break;
                }


        }

        //승리
        static void Victory()
        {
            Console.WriteLine("Victory");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터" + +"마리를 잡았습니다.");
            Console.WriteLine();
            isGameOver = true;
        }

        //패배
        static void Lose()
            {
                Console.WriteLine("You Lose");
                Console.WriteLine();
                isGameOver = true;
            }

            // 몬스터가 죽었는지 확인
        static void CheckMonsters()
            {
                //몬스터 상태 체크
                //모든 몬스터의 체력이 0 이하인지 확인

            }

            // 플레이어 체력 확인
        static void CheckPlayer()
            {
                
            }
}

//전투 결과를 표시
//몬스터나 플레이어 체력이 0 이하인지를 체크
//게임의 결과를 출력합니다.
