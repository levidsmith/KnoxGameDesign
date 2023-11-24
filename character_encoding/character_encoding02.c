#include <stdio.h>

int main(void) {
  char str[5];

  str[0] = 0x40 | 11;
  str[1] = 0x40 | 14;
  str[2] = 0x40 | 15;
  str[3] = 0x40 | 24;
  str[4] = 0;

  printf("%s\n", str);
  return 0;
}
