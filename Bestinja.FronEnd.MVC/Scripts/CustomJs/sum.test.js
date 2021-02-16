
// const sum = require('./sum');

test('mock impelimant',()=>{
const myMockfn = jest.fn(()=>'default')
.mockRejectedValueOnce(()=>'first call')
.mockImplementationOnce(()=>'second call')




  
})