# MachineLearningWebCrawler
Fetch data from third party site and fill our domain


Video Doc

https://www.dropbox.com/s/g7nfumsog6lxznx/Selenium.wmv?dl=0

https://www.dropbox.com/s/mli0o0or97pe5yo/Video_2018-04-04_105927.wmv?dl=0



  public isValidEmail(email:string) :boolean
  {
    this.regexp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
    return this.regexp.test(email);
  }

  public isNumber(number:any) :boolean
  {
    return !isNaN(number);
  }
