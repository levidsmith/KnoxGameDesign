#include <stdio.h>

int recursionMultiply(int val, int iTimes) {
  iTimes--;
  if (iTimes > 0) {
    val = val + recursionMultiply(val, iTimes);
  }

  return val;
}

int main(void) {
  printf("multiply with recursion 3 x 4: %d\n", recursionMultiply(3, 4));
  printf("multiply with recursion 2 x 21: %d\n", recursionMultiply(2, 21));
  printf("multiply with recursion 7 x 17 x 17: %d\n", recursionMultiply(recursionMultiply(7, 17), 17));
  return 0;
}
