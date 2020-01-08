import { ProagilAppPage } from './app.po';

describe('proagil-app App', () => {
  let page: ProagilAppPage;

  beforeEach(() => {
    page = new ProagilAppPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
