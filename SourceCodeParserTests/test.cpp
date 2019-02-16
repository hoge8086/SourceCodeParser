
/*
	メイン関数
	引数はa, b 
 */
int main(int a, int b)
{
	int c = a + b;

	// funcを呼ぶ
	func();
	return 0;
}

//複数行関数

void func(
#define AA "ここでdefineするか???"
	int a,	//a
	int b,  //b
	int c,  //c
	)
{
	int sum;
	for (int i = 0; i < 10; i++)
	{
		sum += i;
	}

	while (a)
	{
		sum += a;
	}

	switch (b)
	{
	case 10:
		break;
	default:
		break;
	}

}

#define A(a, b) \
	((a) + (b))

/*
	int void func2(){}
*/

struct B
{
	int a;
};

class A
{
	int void func1(){};
	int a;

};
