class Pati {
  double latitute;
  double longtitute;
  double weight;
  double percent;

  Pati({this.latitute, this.longtitute, this.weight,this.percent});

  Pati.fromJson(Map<String, dynamic> json) {
    latitute = json['latitute'];
    longtitute = json['longtitute'];
    weight = json['weight'];
    percent = json['percent'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['latitute'] = this.latitute;
    data['longtitute'] = this.longtitute;
    data['weight'] = this.weight;
    data['percent'] = this.percent;
    return data;
  }
}
