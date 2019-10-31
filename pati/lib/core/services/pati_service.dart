import 'package:http/http.dart' as http;

class PatiService {
  static PatiService _instance = PatiService._init();
  PatiService._init();
  static PatiService instance = _instance;

  String baseUrl = "";

  Future<void> getPatiService() async {
    final response = await http.get(baseUrl);  
    return response.body;
  }
}
